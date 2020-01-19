using System;
using System.Threading.Tasks;
using AgentPortal.Domain.Configuration;
using AgentPortal.Domain.Store;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;

namespace AgentPortal.ImageStore
{
    public class AzureBlobImageStore : IImageStore
    {
        private readonly Lazy<CloudStorageAccount> _storageAccountProvider;
        private CloudStorageAccount _cloudStorageAccount => _storageAccountProvider.Value;
        private CloudBlobClient _cloudBlobClient => _cloudStorageAccount.CreateCloudBlobClient();

        public AzureBlobImageStore(IOptionsMonitor<ImageBlobStoreConfiguration> blobStorageOptionsAccessor)
        {
            if (blobStorageOptionsAccessor.CurrentValue == null) throw new ArgumentNullException(nameof(blobStorageOptionsAccessor));

            _storageAccountProvider = new Lazy<CloudStorageAccount>(() => TryCreateStorageAccount(blobStorageOptionsAccessor.CurrentValue.ConnectionString));
        }

        private CloudStorageAccount TryCreateStorageAccount(string connectionString)
        {
            CloudStorageAccount account;
            var createAttempt = CloudStorageAccount.TryParse(connectionString, out account);
            return account;
        }

        public async Task<bool> TryCreateContainer(string container)
        {
            var cloudBlobContainer = GetCloudBlobContainer(container);
            var isNewlyCreated = await cloudBlobContainer.CreateIfNotExistsAsync();

            if (isNewlyCreated)
            {
                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob,
                };
                await cloudBlobContainer.SetPermissionsAsync(permissions);
            }

            return isNewlyCreated;
        }

        private CloudBlobContainer GetCloudBlobContainer(string container)
        {
            
            return _cloudBlobClient.GetContainerReference(container);
        }
    }
}

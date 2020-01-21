using System;
using System.Text;
using System.Threading.Tasks;
using AgentPortal.Domain.Configuration;
using AgentPortal.Domain.Store;
using AgentPortal.Domain.Values;
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
            var imageBlobStoreConfiguration = blobStorageOptionsAccessor.CurrentValue;
            var connectionString = !string.IsNullOrWhiteSpace(imageBlobStoreConfiguration.ConnectionString)
                ? imageBlobStoreConfiguration.ConnectionString
                : Encoding.UTF8.GetString(Convert.FromBase64String(imageBlobStoreConfiguration.Fallback));

            if (imageBlobStoreConfiguration == null) throw new ArgumentNullException(nameof(blobStorageOptionsAccessor));

            _storageAccountProvider = new Lazy<CloudStorageAccount>(() =>
            {
                return TryCreateStorageAccount(connectionString);
            });
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

        public async Task<string> PersistArticleEntryMedia(string imageFileName, byte[] mediaContent, string contentType)
        {
            if (mediaContent == null) throw new ArgumentNullException(nameof(mediaContent));
            if (string.IsNullOrWhiteSpace(contentType)) throw new ArgumentNullException(nameof(contentType));

            var entryMediaBlobContainer = GetCloudBlobContainer(ImageStoreValueObject.BLOB_IMAGE_CONTAINER_NAME);
            //var entryContentBlobDirectory = entryMediaBlobContainer.GetDirectoryReference($"{entryContentId}");
            var entryMediaBlob = entryMediaBlobContainer.GetBlockBlobReference($"{imageFileName}");
            entryMediaBlob.Properties.ContentType = contentType;
            await entryMediaBlob.UploadFromByteArrayAsync(mediaContent, 0, mediaContent.Length);

            return entryMediaBlob.Uri.AbsoluteUri;
        }

        private CloudBlobContainer GetCloudBlobContainer(string container)
        {
            
            return _cloudBlobClient.GetContainerReference(container);
        }
    }
}

using System;
using System.Threading.Tasks;
using AgentPortal.Domain.Store;
using AgentPortal.Domain.Values;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AgentPortal.Configuration.Filters
{
    public class ImageStoreStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                Task.WaitAll(CreateRootImageStorageContainer(builder));
                next(builder);
            };
        }

        private async Task CreateRootImageStorageContainer(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var imageStore = serviceScope.ServiceProvider.GetService<IImageStore>();
                var created = await imageStore.TryCreateContainer(ImageStoreValueObject.BLOB_IMAGE_CONTAINER_NAME);
            }
        }
    }
}
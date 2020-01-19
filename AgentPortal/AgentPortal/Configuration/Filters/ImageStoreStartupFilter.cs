using System;
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
                CreateRootImageStorageContainer(builder);
                next(builder);
            };
        }

        private void CreateRootImageStorageContainer(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var imageStore = serviceScope.ServiceProvider.GetService<IImageStore>();
                imageStore.TryCreateContainer(ImageStoreValueObject.BLOB_IMAGE_CONTAINER_NAME);
            }
        }
    }
}
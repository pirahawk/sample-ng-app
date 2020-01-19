using AgentPortal.Configuration.Filters;
using AgentPortal.Db;
using AgentPortal.Domain.Configuration;
using AgentPortal.Domain.Coordinators;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Store;
using AgentPortal.ImageStore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace AgentPortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ImageBlobStoreConfiguration>(Configuration.GetSection("ImageBlobStoreConfiguration"));

            services.AddTransient<IStartupFilter, DbContextConfigurationStartupFilter>();
            services.AddTransient<IStartupFilter, ImageStoreStartupFilter>();

            services.AddRazorPages();
            services.AddControllers();
            services.AddDbContext<AgentPortalDBContext>(options =>
            {
                options.UseInMemoryDatabase("AgentPortalDb");
            });

            services.AddTransient<IImageStore, AzureBlobImageStore>();
            services.AddTransient<IPortalDbContext, AgentPortalPortalDbContextAdapter>();

            services.AddTransient<IGetAllListingsCoordinator, GetAllListingsCoordinator>();
            services.AddTransient<IFindListingCoordinator, FindListingCoordinator>();
            services.AddTransient<IGetAllImagesCoordinator, GetAllImagesCoordinator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();

                //endpoints.MapHealthChecks("/health");
                endpoints.Map("/{wild:regex(^(?!(api|js)).*$)}/{*url}", async httpContext =>
                {
                    await Task.CompletedTask;
                    httpContext.Response.Redirect("/");
                });
            });
        }
    }
}

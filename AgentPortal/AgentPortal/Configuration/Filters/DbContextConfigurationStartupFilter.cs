using System;
using AgentPortal.Db;
using AgentPortal.Domain.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AgentPortal.Configuration.Filters
{
    public class DbContextConfigurationStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return (builder) =>
            {
                using (var scope = builder.ApplicationServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<AgentPortalDBContext>();
                    var created = dbContext.Database.EnsureCreated();
                    SeedTestData(dbContext);
                }
                next(builder);
            };
        }

        private void SeedTestData(AgentPortalDBContext dbContext)
        {
            dbContext.Listings.Add(new Listing
            {
                Description = "One Hyde Park, 100",
                Address = "100 Knightsbridge, London",
                AskingPrice = 200000000m,
                NumberBedrooms = 10,
                PostCode = "SW1X 7LJ"
            });

            dbContext.Listings.Add(new Listing
            {
                Description = "72 Vincent Square",
                Address = "Westminster, London",
                AskingPrice = 100000000m,
                NumberBedrooms = 8,
                PostCode = "SW1P 2PA"
            });

            dbContext.Listings.Add(new Listing
            {
                Description = "Belgravia Gate",
                Address = "Belgravia, London",
                AskingPrice = 100000000m,
                NumberBedrooms = 5,
                PostCode = "SW1X 7EE"
            });

            dbContext.SaveChanges();
        }
    }
}
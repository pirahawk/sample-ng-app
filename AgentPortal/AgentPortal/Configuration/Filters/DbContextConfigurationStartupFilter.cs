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
            var listing1 = new Listing
            {
                Description = "One Hyde Park, 100",
                Address = "100 Knightsbridge, London",
                AskingPrice = 200000000m,
                NumberBedrooms = 10,
                PostCode = "SW1X 7LJ",
            };
            
            var listing2 = new Listing
            {
                Description = "72 Vincent Square",
                Address = "Westminster, London",
                AskingPrice = 100000000m,
                NumberBedrooms = 8,
                PostCode = "SW1P 2PA",
            };

            var listing3 = new Listing
            {
                Description = "Belgravia Gate",
                Address = "Belgravia, London",
                AskingPrice = 100000000m,
                NumberBedrooms = 5,
                PostCode = "SW1X 7EE"
            };

            var listing4 = new Listing
            {
                Description = "Ashurst Road",
                Address = "Ashurst Road, London",
                AskingPrice = 100000000m,
                NumberBedrooms = 5,
                PostCode = "N12 9AH"
            };

            dbContext.Listings.Add(listing1);
            dbContext.Listings.Add(listing2);
            dbContext.Listings.Add(listing3);
            dbContext.Listings.Add(listing4);

            dbContext.SaveChanges();

            var listingImage1 = new ListingImage
            {
                ListingId = listing1.Id,
                Listing = listing1,
                Url = "https://zooplapirantassessment.blob.core.windows.net/stock-images/1.jpg"
            };

            var listingImage2 = new ListingImage
            {
                ListingId = listing2.Id,
                Listing = listing2,
                Url = "https://zooplapirantassessment.blob.core.windows.net/stock-images/2.jpg"
            };

            var listingImage3 = new ListingImage
            {
                ListingId = listing3.Id,
                Listing = listing3,
                Url = "https://zooplapirantassessment.blob.core.windows.net/stock-images/3.jpg"
            };

            dbContext.Images.Add(listingImage1);
            dbContext.Images.Add(listingImage2);
            dbContext.Images.Add(listingImage3);
            dbContext.SaveChanges();

            BulkLoadData(dbContext, listingImage1);

        }

        private static void BulkLoadData(AgentPortalDBContext dbContext, ListingImage listingImage1)
        {
            for (int i = 0; i < 5; i++)
            {
                var listing = new Listing
                {
                    Description = $"Test Listing {i}",
                    Address = $"{i} Downing Street, London",
                    AskingPrice = i * 10000m,
                    NumberBedrooms = i,
                    PostCode = "SW1X 7EE"
                };

                dbContext.Listings.Add(listing);
                dbContext.SaveChanges();

                for (int j = 0; j < 10; j++)
                {
                    var listingImage = new ListingImage
                    {
                        ListingId = listing.Id,
                        Listing = listing,
                        Url = j % 2 == 0
                            ? "https://zooplapirantassessment.blob.core.windows.net/stock-images/3.jpg"
                            : "https://zooplapirantassessment.blob.core.windows.net/stock-images/3.jpg"
                    };

                    dbContext.Images.Add(listingImage);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
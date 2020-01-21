using System.Collections.Generic;
using AgentPortal.Domain.Coordinators;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Tests.Data;
using Xunit;

namespace AgentPortal.Domain.Tests.Coordinators
{
    public class ListingValidatorHelperTest
    {
        public static IEnumerable<object[]> ValidationTestCases
        {
            get
            {
                var listingWithNoAddess = new ListingFixture { Address = string.Empty}.Build();
                yield return new object[]{listingWithNoAddess, false};

                var listingWithNoAskingPrice = new ListingFixture { AskingPrice = 0 }.Build();
                yield return new object[] { listingWithNoAskingPrice, false };

                var listingWithNegativeAskingPrice = new ListingFixture { AskingPrice = -1 }.Build();
                yield return new object[] { listingWithNegativeAskingPrice, false };

                var listingWithNoDescription = new ListingFixture { Description = string.Empty }.Build();
                yield return new object[] { listingWithNoDescription, false };

                var listingWithNoBedrooms = new ListingFixture { NumberBedrooms = 0 }.Build();
                yield return new object[] { listingWithNoBedrooms, false };

                var listingWithNegativeBedrooms = new ListingFixture { NumberBedrooms = -1 }.Build();
                yield return new object[] { listingWithNegativeBedrooms, false };

                var validListing = new ListingFixture ().Build();
                yield return new object[] { validListing, true };
            }
        }

        public static IEnumerable<object[]> UkPostCodesTestData
        {
            get
            {
                yield return new object[] { "M1 1AA", true };
                yield return new object[] { "M60 1NW", true };
                yield return new object[] { "CR2 6XH", true };
                yield return new object[] { "DN55 1PT", true };
                yield return new object[] { "W1P 1HQ", true };
                yield return new object[] { "EC1A 1BB", true };
                yield return new object[] { "EC1A1BB", true };
                yield return new object[] { "ec1a1bb", true };
                yield return new object[] { "ec1a 1bb", true };

                yield return new object[] { "", false };
                yield return new object[] { "6M0 1NW", false };
                yield return new object[] { "6M01NW", false };
                yield return new object[] { "tooManyLetters", false };
                yield return new object[] { "ec1a $bb", false };
            }
        }

        [Theory]
        [MemberData(nameof(ValidationTestCases))]
        public void ValidatesListingAsExpected(Listing listingToValidate, bool expectedValid)
        {
            var result = new ListingValidatorHelper().HasValidFields(listingToValidate);
            Assert.Equal(expectedValid, result);
        }

        [Theory]
        [MemberData(nameof(UkPostCodesTestData))]
        public void ValidatesPostCodesAsExpected(string postCodeToValidate, bool expectedValid)
        {
            var listing = new ListingFixture { PostCode = postCodeToValidate}.Build();
            var result = new ListingValidatorHelper().HasValidFields(listing);
            Assert.Equal(expectedValid, result);
        }
    }
}
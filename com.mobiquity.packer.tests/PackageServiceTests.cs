using com.mobiquity.packer.Services;
using Xunit;

namespace com.mobiquity.packer.tests
{
    /// <summary>
    /// To test the Package Service
    /// </summary>
    public class PackageServiceTests
    {
        private readonly PackageService _sut;

        public PackageServiceTests()
        {
            _sut = new PackageService();
        }

        [Theory]
        [InlineData(81, "81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)")]
        [InlineData(8, "8 : (1,15.3,€34)")]
        [InlineData(75, "75 : (1,85.31,€29) (2,14.55,€74) (3,3.98,€16) (4,26.24,€55) (5,63.69,€52) (6,76.25,€75) (7,60.02,€74) (8,93.18,€35) (9,89.95,€78)")]
        [InlineData(56, "56 : (1,90.72,€13) (2,33.80,€40) (3,43.15,€10) (4,37.97,€16) (5,46.81,€36) (6,48.77,€79) (	7,81.80,€45) (8,19.36,€79) (9,6.76,€64)")]
        public void WeightLimitShouldBeEqualForCreatedPackage(int weightLimit, string line)
        {
            var package = _sut.CreatePackage(line);

            Assert.Equal(weightLimit, package.WeightLimit);
        }


        [Theory]
        [InlineData("105 : (1,53.38,€45)")]
        [InlineData("111 : (1,15.3,€34)")]
        [InlineData("-1 : (1,85.31,€29) (2,14.55,€74)")]
        public void WeightLimitShouldNotBeGreaterThan100OrLessThan0(string line)
        {
            Assert.Throws<APIException>(() => _sut.CreatePackage(line));
        }

        [Theory]
        [InlineData("81 : (1,100.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)")]
        [InlineData("8 : (1,100.01,€34)")]
        [InlineData("75 : (1,85.31,€29) (2,14.55,€74) (3,3.98,€16) (4,26.24,€55) (5,63.69,€52) (6,76.25,€75) (7,60.02,€74) (8,93.18,€35) (9,101.22,€78)")]
        public void ItemWeightShouldNotBeGreaterThan100(string line)
        {
            Assert.Throws<APIException>(() => _sut.CreatePackage(line));
        }

        [Theory]
        [InlineData("81 : (1,53.38,€101) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)")]
        [InlineData("8 : (1,15.3,€109)")]
        public void ItemCostShouldNotBeGreaterThan100(string line)
        {
            Assert.Throws<APIException>(() => _sut.CreatePackage(line));
        }

        [Theory]
        [InlineData("81 : (1,53.38,101) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)")]
        [InlineData("81 : (1,53.38,101€) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)")]
        public void ItemCostShouldBeValid(string line)
        {
            Assert.Throws<APIException>(() => _sut.CreatePackage(line));
        }

        [Theory]
        [InlineData("81 : (1,53.38,101) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48) (7,53.38,101) (8,88.62,€98) (9,78.48,€3) (10,72.30,€76) (11,30.18,€9) (12,46.34,€48) (13,53.38,101) (14,88.62,€98) (15,78.48,€3) (16,72.30,€76)")]
        public void ItemListShouldNotBeMoreThan15(string line)
        {
            Assert.Throws<APIException>(() => _sut.CreatePackage(line));
        }

    }
}

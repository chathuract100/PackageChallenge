using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services;
using System.Collections.Generic;
using Xunit;

namespace com.mobiquity.packer.tests
{
    /// <summary>
    /// To test the response service
    /// </summary>
    public class ResponseServiceTests
    {
        private readonly ResponseService _sut;

        public ResponseServiceTests()
        {
            _sut = new ResponseService();
        }

        [Fact]  
        public void ResponseShouldMatchOutput()
        {
            //Arrange
            List<Package> PackageList = new();
            Package package1 = new()
            {
                WeightLimit = 81,
                PackageItems =
                    new PackageItem[] { new PackageItem(1, (float)53.38, 45) }
            };
            Package package2 = new()
            {
                WeightLimit = 99,
                PackageItems =
                    new PackageItem[] { new PackageItem(1, (float)53.38, 45),
                                        new PackageItem(2, (float)88.62, 98)}
            };

            PackageList.Add(package1);
            PackageList.Add(package2);

            //Act
            var result = _sut.PrintResponse(PackageList);

            //Assert
            Assert.Equal("1\r\n1,2\r\n", result);
        }
    }
}

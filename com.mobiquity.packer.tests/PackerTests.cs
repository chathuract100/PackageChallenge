using System;
using System.IO;
using Xunit;

namespace com.mobiquity.packer.tests
{
    /// <summary>
    /// To test the Packer Service
    /// </summary>
    public class PackerTests
    {
        [Theory]
        [InlineData("example_input")]
        public void PackerShouldReturnString(string fileName)
        {
            var returnValue = Packer.Pack(Path.Combine(Environment.CurrentDirectory, "Resources", fileName));

            Assert.True(typeof(string) == returnValue.GetType());
        }

        [Theory]
        [InlineData("WrongFileName")]
        public void WrongFileNameShouldReturnException(string fileName)
        {
            Assert.Throws<APIException>(() => Packer.Pack(Path.Combine(Environment.CurrentDirectory, "Resources", fileName)));
        }

        [Theory]
        [InlineData("example_input", "4\r\n-\r\n2,7\r\n8,9\r\n")]
        [InlineData("example_input2", "2,7\r\n8,9\r\n")]
        [InlineData("example_input3", "4\r\n1\r\n2,3\r\n8,9\r\n")]
        public void PackerResponseShouldMatch(string fileName, string expected)
        {
            var returnValue = Packer.Pack(Path.Combine(Environment.CurrentDirectory, "Resources", fileName));

            Assert.Equal(expected, returnValue);
        }

        [Theory]
        [InlineData("example_input_wrongPackageItem")]
        public void WrongPackageItemRecordShouldThrowException(string fileName)
        {
            Assert.Throws<APIException>(() => Packer.Pack(Path.Combine(Environment.CurrentDirectory, "Resources", fileName)));
        }
    }
}

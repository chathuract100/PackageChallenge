using com.mobiquity.packer.Services;
using System;
using System.IO;
using Xunit;

namespace com.mobiquity.packer.tests
{
    /// <summary>
    /// To Test the File service
    /// </summary>
    public class FileServiceTests
    {
        private readonly FileService _sut;

        public FileServiceTests()
        {
            _sut = new FileService();
        }

        [Theory]
        [InlineData("example_input")]
        [InlineData("example_input2")]
        [InlineData("example_input3")]
        [InlineData("example_output")]
        public void CorrectFileNameShouldReturnTrue(string fileName)
        {
            //Arrange
            var FullPath = Path.Combine(Environment.CurrentDirectory, "Resources", fileName);

            //Act
            var result = _sut.IsExist(FullPath);

            //Assert
            Assert.True(result);

        }

        [Theory]
        [InlineData("WrongFileName1")]
        [InlineData("WrongFileName2")]
        public void WrongtFileNameShouldReturnTrue(string fileName)
        {
            var FullPath = Path.Combine(Environment.CurrentDirectory, "Resources", fileName);

            var result = _sut.IsExist(FullPath);

            Assert.False(result);

        }

        [Fact]  
        public void EmptyFilePathShouldReturnException()
        {
            string EmptyFilePath = "";

            Assert.Throws<APIException>(() => _sut.IsExist(EmptyFilePath));
        }

        [Theory]
        [InlineData("example_input", 4)]
        [InlineData("example_input2", 2)]
        public void NumberOfLinesCountShouldMatch(string fileName, int expectedNumberOfLines)
        {
            var FullPath = Path.Combine(Environment.CurrentDirectory, "Resources", fileName);

            var Lines = _sut.ReadFile(FullPath);

            Assert.Equal(expectedNumberOfLines, Lines.Length);
        }

        [Theory]
        [InlineData("example_input", "81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)")]
        [InlineData("example_input2", "75 : (1,85.31,€29) (2,14.55,€74) (3,3.98,€16) (4,26.24,€55) (5,63.69,€52) (6,76.25,€75) (7,60.02,€74) (8,93.18,€35) (9,89.95,€78)")]
        public void FileLineContentShouldMatch(string fileName, string firstLineContent)
        {
            var FullPath = Path.Combine(Environment.CurrentDirectory, "Resources", fileName);

            var Lines = _sut.ReadFile(FullPath);

            Assert.Equal(firstLineContent, Lines[0]);
        }

        [Theory]
        [InlineData("empty_file_input")]
        public void EmptyFileShouldReturnEmptyArray(string fileName)
        {
            var FullPath = Path.Combine(Environment.CurrentDirectory, "Resources", fileName);

            var Lines = _sut.ReadFile(FullPath);

            Assert.Empty(Lines);
        }
    }
}

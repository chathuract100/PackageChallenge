using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services;
using System.Collections.Generic;

namespace com.mobiquity.packer
{
    public static class Packer
    {
        private static readonly IFileService _fileService = new FileService();
        private static readonly IPackageService _packageService = new PackageService();
        private static readonly IWrappingService _wrappingService = new WrappingService();
        private static readonly IResponseService _responseService = new ResponseService();

        /// <summary>
        /// Method to optimize the packaging and return the output
        /// </summary>
        /// <param name="filePath">path of the file</param>
        /// <returns>string representation of the optimized package list</returns>
        /// <exception cref="APIException"></exception>
        public static string Pack(string filePath)
        {
            if (!_fileService.IsExist(filePath))
            {
                throw new APIException("Error: File at " + filePath + " does not exist !");
            }
            
            string[] allLines = _fileService.ReadFile(filePath);

            List<Package> packagelist = _packageService.CreatePackageList(allLines);

            List<Package> optimizedPackageList = _wrappingService.Wrap(packagelist);

            return _responseService.PrintResponse(optimizedPackageList);

        }
    }
}

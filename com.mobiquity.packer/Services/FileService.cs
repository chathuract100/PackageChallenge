using System.IO;
using System.Text;

namespace com.mobiquity.packer.Services
{
    /// <summary>
    /// Class to implement logic for file reader
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Method to check if the file exists
        /// </summary>
        /// <param name="path">path of the file</param>
        /// <returns>Whether the file exists or not</returns>
        /// <exception cref="APIException"></exception>
        public bool IsExist(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new APIException("Error: The path cannot be empty !");
            }
            return File.Exists(path);
        }

        /// <summary>
        /// Method to read all lines of the file
        /// </summary>
        /// <param name="path">path of the file</param>
        /// <returns>string array of the lines in the file</returns>
        public string[] ReadFile(string path)
        {
            return File.ReadAllLines(path, Encoding.UTF8);
        }
    }
}

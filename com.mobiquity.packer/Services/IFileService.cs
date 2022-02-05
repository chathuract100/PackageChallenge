using System;

namespace com.mobiquity.packer.Services
{
    public interface IFileService
    {
        bool IsExist(string path);

        string[] ReadFile(string path);
    }
}

using com.mobiquity.packer.Models;
using System.Collections.Generic;

namespace com.mobiquity.packer.Services
{
    public interface IPackageService
    {
        List<Package> CreatePackageList(string[] lines);
        Package CreatePackage(string itemLine);
        PackageItem CreatePackageItem(string item);
    }
}

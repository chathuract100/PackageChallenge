using System;

namespace com.mobiquity.packer.Models
{
    /// <summary>
    /// Class to represent the package
    /// </summary>
    public class Package
    {
        public int WeightLimit { get; set; }
        public PackageItem[] PackageItems { get; set; }
    }
}

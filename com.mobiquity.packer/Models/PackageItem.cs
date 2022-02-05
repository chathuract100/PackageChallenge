using System;

namespace com.mobiquity.packer.Models
{
    /// <summary>
    /// Class to represent an item in the package
    /// </summary>
    public class PackageItem
    {
        public int Index { get; private set; }
        public float Weight { get; private set; }
        public int Cost { get; private set; }

        public PackageItem(int index, float weight, int cost)
        {
            if (weight > 100)
            {
                throw new APIException("Error: weight cannot be greater than 100");
            }
            if (cost > 100)
            {
                throw new APIException("Error: cost cannot be greater than 100");
            }

            this.Index = index;
            this.Weight = weight;
            this.Cost = cost;

        }
    }
}

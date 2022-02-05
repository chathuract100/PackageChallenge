using com.mobiquity.packer.Models;
using com.mobiquity.packer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Services
{
    /// <summary>
    /// Class to implement logic for mapping to package models
    /// </summary>
    public class PackageService : IPackageService
    {
        public List<Package> CreatePackageList(string[] lines)
        {
            List<Package> packageList = new();
            foreach (string line in lines)
            {
                packageList.Add(CreatePackage(line));
            }
            return packageList;

        }

        public Package CreatePackage(string itemLine)
        {
            Package package = new();
            string[] weightItemArray = itemLine.Split(Constants.PackageMaxWeightSeperator, StringSplitOptions.None);

            // Validating for invalid package line
            if(weightItemArray.Length != 2)
                throw new APIException("Error: Invalid Record found !");
            // Validating and assigning package Weight limit
            if(!int.TryParse(weightItemArray[0].Trim(), out int packageWeightLimit))
                throw new APIException("Error: Package weight Limit corrupted !");
            // Validating package weight limit contraint
            if (packageWeightLimit <= 0 || packageWeightLimit >= 100)
                throw new APIException("Error: Invalid Weight limit for the package !");

            package.WeightLimit = packageWeightLimit;
            string[] itemsStringArr = weightItemArray[1].Trim().Split(Constants.PackageItemSeperator, StringSplitOptions.None);

            //Validating max item number constraint
            if(itemsStringArr.Length > 15)
                throw new APIException("There cannot be more than 15 items to choose from. Error on item record with maxWeight: " + package.WeightLimit);

            package.PackageItems = itemsStringArr.Select(x => CreatePackageItem(x[1..^1])).ToArray();

            return package;
        }

        public PackageItem CreatePackageItem(string items)
        {
            string[] itemArray = items.Split(Constants.ItemPropertySeperator, StringSplitOptions.None);

            if (itemArray.Length != 3)
                throw new APIException("Error: Package item record is corrupted");

            if (!int.TryParse(itemArray[0], out int index))
                throw new APIException("Error: Package item index error");
            if (!float.TryParse(itemArray[1], out float weight))
                throw new APIException("Error: Package item weight error");
            if (!itemArray[2].StartsWith(Constants.EuroPrefix))
                throw new APIException("Error: Package item cost is invalid");
            if (!int.TryParse(itemArray[2].Remove(0, 1), out int cost))
                throw new APIException("Error: Package item cost error");

            return new PackageItem(index, weight, cost);
        }

    }
}

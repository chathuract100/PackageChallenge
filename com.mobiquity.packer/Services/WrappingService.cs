using com.mobiquity.packer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Services
{
    /// <summary>
    /// Class to implement logic to handle the wrapping of the package
    /// </summary>
    public class WrappingService : IWrappingService
    {
        /// <summary>
        /// Method to wrap the package list with optimized packages
        /// </summary>
        /// <param name="packageList">package list to be optimized</param>
        /// <returns>Optimized package list</returns>
        public List<Package> Wrap(List<Package> packageList)
        {
            return packageList.Select(x =>
            {
                return PackageOptimizer(x);
            }).ToList();

        }

        /// <summary>
        /// Method to optimize the package
        /// </summary>
        /// <param name="package"> package to be optimized </param>
        /// <returns>optimized package with items</returns>
        private static Package PackageOptimizer(Package package)
        {
            // ordering by Weight to pick the less weight item when there are more than 1 items with same price
            package.PackageItems = package.PackageItems.OrderBy(x => x.Weight).ToArray();

            int optimalCost = 0;
            Package optimizedPackage = new()
            {
                WeightLimit = package.WeightLimit
            };

            int[] weights = { 0 };
            weights = weights.Concat(package.PackageItems.Select( x => (int)x.Weight)).ToArray();
            int[] costs = { 0 };
            costs = costs.Concat(package.PackageItems.Select(x => x.Cost)).ToArray();
            

            // array to hold the weights and costs
            int[,] data = new int[package.PackageItems.Length + 1, package.WeightLimit + 1];

            int n = package.PackageItems.Length;

            int maxWeight = package.WeightLimit;

            for(int itemNum = 0; itemNum <= n; itemNum++)
            {
                for(int weight = 0; weight <= maxWeight; weight++)
                {
                    if (itemNum == 0 || weight == 0)
                    {
                        data[itemNum, weight] = 0;
                    }
                    else if (weights[itemNum] <= weight)
                    {
                        data[itemNum, weight] = Math.Max(costs[itemNum] + data[itemNum - 1, weight - weights[itemNum]], data[itemNum - 1, weight]);
                    }
                    else
                    {
                        data[itemNum, weight] = data[itemNum - 1, weight];
                    }
                }
            }

            // The optimal cost to be included in the package
            optimalCost = data[n, maxWeight];

            optimizedPackage.PackageItems = GetIncludedPackageItems(n, maxWeight, weights, costs, package, data).OrderBy(x => x.Index).ToArray();

            return optimizedPackage;
        }

        /// <summary>
        /// Method to find the included items
        /// </summary>
        /// <param name="n">Item Count</param>
        /// <param name="maxWeight">Max Weight</param>
        /// <param name="weights">weights collection</param>
        /// <param name="values"> values collection</param>
        /// <param name="packageToOptimize">the package to optimiz </param>
        /// <param name="data">data collection with weights and values</param>
        /// <returns>optimized package item list</returns>
        private static List<PackageItem> GetIncludedPackageItems(int n, int maxWeight, int[] weights, int[] values, Package packageToOptimize, int[,] data)
        {
            List<PackageItem> result = new();

            int i = n;
            int j = maxWeight;

            while (i > 0 && j > 0)
            {
                if (data[i, j] != data[i - 1, j])
                {
                    // item added to the package
                    result.Add(packageToOptimize.PackageItems[i - 1]);
                    j -= weights[i];
                }
                i--;
            }
            return result;
        }
    }
}

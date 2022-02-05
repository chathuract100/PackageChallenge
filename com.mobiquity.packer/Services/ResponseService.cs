using com.mobiquity.packer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Services
{
    /// <summary>
    /// Class to implement logic to handle response
    /// </summary>
    public class ResponseService : IResponseService
    {
        public string PrintResponse(List<Package> packages)
        {
            string returnValue = "";

            foreach (Package package in packages)
            {
                if (package.PackageItems.Length > 0)
                {
                    returnValue += string.Join(",", package.PackageItems.Select(x => x.Index.ToString()));
                }
                else
                {
                    returnValue += "-";
                }
                
                returnValue += Environment.NewLine;

            }

            return returnValue;
        }
    }
}

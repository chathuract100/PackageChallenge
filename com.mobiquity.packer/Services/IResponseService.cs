using com.mobiquity.packer.Models;
using System.Collections.Generic;

namespace com.mobiquity.packer.Services
{
    public interface IResponseService
    {
        string PrintResponse(List<Package> packages);
    }
}

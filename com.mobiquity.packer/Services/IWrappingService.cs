using com.mobiquity.packer.Models;
using System.Collections.Generic;

namespace com.mobiquity.packer.Services
{
    public interface IWrappingService
    {
        List<Package> Wrap(List<Package> package);

    }
}

using System.Collections.Generic;

namespace NugetTest.NuspecCreator.CsprojFileFinder.PackagesFinder
{
    public interface I_PackagesFinder
    {
        IEnumerable<Cl_ProjectInfo> GetPackages(string vrpCsprojText);
    }
}
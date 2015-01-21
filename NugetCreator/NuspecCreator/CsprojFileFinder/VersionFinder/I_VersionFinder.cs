using System;

namespace NugetTest.NuspecCreator.CsprojFileFinder.VersionFinder
{
    public interface I_VersionFinder
    {
        String GetVersion(string vrpCsprojFile, string vrpAssemblyName);
    }
}
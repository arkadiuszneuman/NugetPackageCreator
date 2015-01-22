using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using NugetTest.CsprojDllGetter;

namespace NugetTest.NuspecCreator.CsprojFileFinder.VersionFinder
{
    public class Cl_VersionFinder : I_VersionFinder
    {
        private readonly I_CsprojDllGetter vrcCsprojDllGetter;

        public Cl_VersionFinder(I_CsprojDllGetter vrpCsprojDllGetter)
        {
            vrcCsprojDllGetter = vrpCsprojDllGetter;
        }

        public String GetVersion(string vrpCsprojFile, string vrpAssemblyName)
        {
            string vrlDllFileFromCsproj = vrcCsprojDllGetter.GetDllDirectoryFromCsproj(vrpCsprojFile);

            string vrlPath = Path.Combine(vrlDllFileFromCsproj, vrpAssemblyName + ".dll");
            Assembly vrlAssembly = Assembly.LoadFile(vrlPath);
            Version vrlVersion = vrlAssembly.GetName().Version;

            return vrlVersion.ToString();
        }
    }
}
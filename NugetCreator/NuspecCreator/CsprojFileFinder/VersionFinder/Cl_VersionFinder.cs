using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace NugetTest.NuspecCreator.CsprojFileFinder.VersionFinder
{
    public class Cl_VersionFinder : I_VersionFinder
    {
        public String GetVersion(string vrpCsprojFile, string vrpAssemblyName)
        {
            string vrlPath = Path.GetDirectoryName(vrpCsprojFile) + @"\bin\Release\" + vrpAssemblyName + ".dll";
            Assembly vrlAssembly = Assembly.Load(vrlPath);
            Version vrlVersion = vrlAssembly.GetName().Version;

            return vrlVersion.ToString();
        }
    }
}
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace NugetTest.CsprojFileFinder.DependencyFinder
{
    public class Cl_AssemblyNameFinder
    {
        public string GetAssemblyName(string vrpCsprojText)
        {
            XElement vrlRoot = XElement.Load(new StringReader(vrpCsprojText));
            return vrlRoot.Elements().First(el => el.Name.LocalName == "PropertyGroup").Elements().Single(el => el.Name.LocalName == "AssemblyName").Value;
        }
    }
}
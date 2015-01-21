using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace NugetTest.NuspecCreator.CsprojFileFinder.PackagesFinder
{
    public class Cl_PackagesFinder
    {
        public IEnumerable<Cl_ProjectInfo> GetPackages(string vrpCsprojText)
        {
            XElement vrlRoot = XElement.Load(new StringReader(vrpCsprojText));
            foreach (XElement vrlReferenceInclude in vrlRoot.Elements().First(el => el.Name.LocalName == "ItemGroup").Elements().Where(r => r.Name.LocalName == "Reference" && r.Elements().Select(el => el.Name.LocalName).Contains("HintPath")))
            {
                string vrlReferenceName = vrlReferenceInclude.Attribute("Include").Value.Split(',').First();

                XElement vrlHintPath = vrlReferenceInclude.Elements().Single(e => e.Name.LocalName == "HintPath");
                string vrlPath = vrlHintPath.Value;
                string vrlAssembyDir = vrlPath.Split('\\')[2];
                string vrlVersion = vrlAssembyDir.Replace(vrlReferenceName + '.', "");

                yield return new Cl_ProjectInfo(vrlReferenceName, vrlVersion);
            }
        }
    }
}
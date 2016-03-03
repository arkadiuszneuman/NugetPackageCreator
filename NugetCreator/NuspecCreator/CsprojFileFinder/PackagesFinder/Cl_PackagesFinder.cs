using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace NugetTest.NuspecCreator.CsprojFileFinder.PackagesFinder
{
    public class Cl_PackagesFinder : I_PackagesFinder
    {
        public IEnumerable<Cl_ProjectInfo> GetPackages(string vrpCsprojText)
        {
            XElement vrlRoot = XElement.Load(new StringReader(vrpCsprojText));
            foreach (XElement vrlReferenceInclude in vrlRoot.Elements().First(el => el.Name.LocalName == "ItemGroup").Elements().Where(r => r.Name.LocalName == "Reference" && r.Elements().Select(el => el.Name.LocalName).Contains("HintPath")))
            {
                string vrlReferenceName = vrlReferenceInclude.Attribute("Include").Value.Split(',').First();

                XElement vrlHintPath = vrlReferenceInclude.Elements().SingleOrDefault(e => e.Name.LocalName == "HintPath");
                if (vrlHintPath != null && vrlHintPath.Value.StartsWith(@"..\packages\"))
                {
                    if (!vrlReferenceName.StartsWith("InsERT.") && !vrlReferenceName.StartsWith("EntityFramework.SqlServer"))
                    {
                        string vrlPath = vrlHintPath.Value;
                        string vrlAssembyDir = vrlPath.Split('\\')[2];
                        string vrlVersion = vrlAssembyDir.Replace(vrlReferenceName + '.', "");

                        yield return new Cl_ProjectInfo(vrlReferenceName, vrlVersion);
                    }
                }
            }
        }
    }
}
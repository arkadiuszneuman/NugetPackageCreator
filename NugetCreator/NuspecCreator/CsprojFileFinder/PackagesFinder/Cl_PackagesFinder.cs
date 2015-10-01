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

                XElement vrlPrivate = vrlReferenceInclude.Elements().SingleOrDefault(e => e.Name.LocalName == "Private");
                if (vrlPrivate == null || vrlPrivate.Value.ToLower() == "false")
                {
                    if (!vrlReferenceName.StartsWith("InsERT.") && !vrlReferenceName.StartsWith("EntityFramework.SqlServer"))
                    {
                        XElement vrlHintPath = vrlReferenceInclude.Elements().Single(e => e.Name.LocalName == "HintPath");
                        string vrlPath = vrlHintPath.Value;
                        string vrlAssembyDir = vrlPath.Split('\\')[2];
                        string vrlVersion = vrlAssembyDir.Replace(vrlReferenceName + '.', "");

                        yield return new Cl_ProjectInfo(vrlReferenceName, vrlVersion);
                    }
                }
            }

            yield return new Cl_ProjectInfo("Microsoft.Bcl", "1.1.10");
            yield return new Cl_ProjectInfo("Microsoft.Bcl.Async", "1.0.168");
            yield return new Cl_ProjectInfo("Microsoft.Bcl.Build", "1.0.21");
        }
    }
}
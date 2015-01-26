using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace NugetTest.NuspecCreator.CsprojFileFinder.ProjectReferences
{
    public class Cl_ProjectReferencesFinder : I_ProjectReferencesFinder
    {
        public IEnumerable<Cl_ProjectInfo> GetProjectReferences(string vrpCsprojText)
        {
            XElement vrlRoot = XElement.Load(new StringReader(vrpCsprojText));
            foreach (var vrlProjectReference in vrlRoot.Elements().Where(el => el.Name.LocalName == "ItemGroup").SelectMany(e => e.Elements()).Where(r => r.Name.LocalName == "ProjectReference"))
            {
                string vrlDependencyName = vrlProjectReference.Elements().Single(e => e.Name.LocalName == "Name").Value;
                yield return new Cl_ProjectInfo(vrlDependencyName);
            }
        }
    }
}
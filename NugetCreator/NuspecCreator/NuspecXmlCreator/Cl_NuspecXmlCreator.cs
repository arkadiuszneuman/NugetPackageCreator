using System.IO;
using System.Xml.Linq;
using NugetTest.NuspecCreator.CsprojFileFinder;

namespace NugetTest.NuspecCreator.NuspecXmlCreator
{
    public class Cl_NuspecXmlCreator : I_NuspecXmlCreator
    {
        public string CreateNuspecText(Cl_NuspecProjectInfo vrpNuspecProjectInfo)
        {
            XNamespace vrlNamespace = "http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd";

            XDocument vrlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "no"),
                new XElement(vrlNamespace + "package",
                    new XElement(vrlNamespace + "metadata",
                        new XElement(vrlNamespace + "id", vrpNuspecProjectInfo.ApplicationId),
                        new XElement(vrlNamespace + "version", vrpNuspecProjectInfo.Version),
                        new XElement(vrlNamespace + "title", vrpNuspecProjectInfo.Title),
                        new XElement(vrlNamespace + "authors", vrpNuspecProjectInfo.Authors),
                        new XElement(vrlNamespace + "requireLicenseAcceptance", "false"),
                        new XElement(vrlNamespace + "description", vrpNuspecProjectInfo.Description),
                        new XElement(vrlNamespace + "language"),
                        new XElement(vrlNamespace + "dependencies",
                            CreateGroupElement(vrlNamespace, vrpNuspecProjectInfo))
                    ),
                    new XElement(vrlNamespace + "files",
                        new XElement(vrlNamespace + "file",
                            new XAttribute("src", vrpNuspecProjectInfo.ApplicationId + '.' + vrpNuspecProjectInfo.Version + ".dll"),
                            new XAttribute("target", @"lib\net40\" + vrpNuspecProjectInfo.ApplicationId + '.' + vrpNuspecProjectInfo.Version + ".dll")),
                        new XElement(vrlNamespace + "file",
                            new XAttribute("src", vrpNuspecProjectInfo.ApplicationId + '.' + vrpNuspecProjectInfo.Version + ".xml"),
                            new XAttribute("target", @"lib\net40\" + vrpNuspecProjectInfo.ApplicationId + '.' + vrpNuspecProjectInfo.Version + ".xml")))
                )
            );

            using (var vrlSw = new MemoryStream())
            {
                using (var vrlStrw = new StreamWriter(vrlSw, System.Text.UTF8Encoding.UTF8))
                {
                    vrlDocument.Save(vrlStrw);
                    return System.Text.UTF8Encoding.UTF8.GetString(vrlSw.ToArray()).Replace(" standalone=\"no\"", "").Remove(0, 1);
                }
            }
        }

        private XElement CreateGroupElement(XNamespace vrpNamespace, Cl_NuspecProjectInfo vrpNuspecProjectInfo)
        {
            XElement vrlElement = new XElement(vrpNamespace + "group",
                new XAttribute("targetFramework", ".NETFramework4.0"));

            foreach (Cl_ProjectInfo vrlProjectReference in vrpNuspecProjectInfo.ProjectReferences)
            {
                vrlElement.Add(new XElement(vrpNamespace + "dependency", 
                    new XAttribute("id", vrlProjectReference.Name),
                    new XAttribute("version",  vrlProjectReference.Version)));
            }

            foreach (Cl_ProjectInfo vrlProjectReference in vrpNuspecProjectInfo.AddionalPackages)
            {
                vrlElement.Add(new XElement(vrpNamespace + "dependency",
                    new XAttribute("id", vrlProjectReference.Name),
                    new XAttribute("version", vrlProjectReference.Version)));
            }
            
            return vrlElement;
        }
    }
}
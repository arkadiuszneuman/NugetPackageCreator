using System.Collections.Generic;
using NugetTest.NuspecCreator.CsprojFileFinder;

namespace NugetTest.NuspecCreator.NuspecXmlCreator
{
    public class Cl_NuspecProjectInfo
    {
        public string ApplicationId { get; set; }
        public string NugetVersion { get; set; }
        public string FileVersion { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }

        public IEnumerable<Cl_ProjectInfo> ProjectReferences { get; set; }
        public IEnumerable<Cl_ProjectInfo> AddionalPackages { get; set; }
    }
}
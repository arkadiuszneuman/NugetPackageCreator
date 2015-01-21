namespace NugetTest.CsprojFileFinder
{
    public class Cl_ProjectInfo
    {
        public Cl_ProjectInfo(string vrpName, string vrpVersion = "")
        {
            Name = vrpName;
            Version = vrpVersion;
        }

        public string Name { get; set; }
        public string Version { get; set; }
    }
}
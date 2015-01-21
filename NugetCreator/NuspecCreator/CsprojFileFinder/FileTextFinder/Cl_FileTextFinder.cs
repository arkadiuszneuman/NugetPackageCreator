using System.IO;

namespace NugetTest.NuspecCreator.CsprojFileFinder.FileTextFinder
{
    public interface I_FileTextFinder
    {
        string GetTextFromFile(string vrpFilePath);
    }

    public class Cl_FileTextFinder : I_FileTextFinder
    {
        public string GetTextFromFile(string vrpFilePath)
        {
            return File.ReadAllText(vrpFilePath);
        }
    }
}
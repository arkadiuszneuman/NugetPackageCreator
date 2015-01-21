using System.IO;
using NugetTest.NuspecCreator.CsprojFileFinder.FileTextFinder;

namespace NugetCreatorTest
{
    public class TextGetter
    {
        public string GetCsprojText()
        {
            return new Cl_FileTextFinder().GetTextFromFile(Path.Combine(Directory.GetCurrentDirectory(), "ExampleCsproj.txt"));
        }
    }
}
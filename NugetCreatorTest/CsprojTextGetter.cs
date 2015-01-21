using System.IO;
using NugetTest.NuspecCreator.FileTextLoader;

namespace NugetCreatorTest
{
    public class TextGetter
    {
        public string GetCsprojText()
        {
            return new Cl_FileTextLoader().GetTextFromFile(Path.Combine(Directory.GetCurrentDirectory(), "ExampleCsproj.txt"));
        }
    }
}
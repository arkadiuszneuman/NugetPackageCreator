using System.IO;

namespace NugetTest.CsprojDllGetter
{
    public class Cl_CsprojDllGetter : I_CsprojDllGetter
    {
        public string GetDllDirectoryFromCsproj(string vrpCsprojPath)
        {
            string vrlDir = Path.GetDirectoryName(vrpCsprojPath) + @"\bin\Release";
            return vrlDir;
        }
    }
}
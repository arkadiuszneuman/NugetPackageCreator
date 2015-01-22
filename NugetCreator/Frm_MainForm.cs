using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NugetTest.CsprojDllGetter;
using NugetTest.NuspecCreator;
using NugetTest.NuspecCreator.CsprojFileFinder.AssemblyNameFinder;
using NugetTest.NuspecCreator.CsprojFileFinder.PackagesFinder;
using NugetTest.NuspecCreator.CsprojFileFinder.ProjectReferences;
using NugetTest.NuspecCreator.CsprojFileFinder.VersionFinder;
using NugetTest.NuspecCreator.FileTextLoader;
using NugetTest.NuspecCreator.FileTextSaver;
using NugetTest.NuspecCreator.NuspecXmlCreator;

namespace NugetTest
{
    public partial class Frm_MainForm : Form
    {
        private List<string> lines = new List<string>();

        public Frm_MainForm()
        {
            InitializeComponent();
        }

        private IEnumerable<string> FindCsProjs()
        {
            var vrlDir = Path.GetDirectoryName(frtxtSlnPath.Text);

            return Directory.GetFiles(vrlDir, "*.csproj", SearchOption.AllDirectories);
        }

        private IEnumerable<string> GetDisabledProjects()
        {
            yield return "AppFrameExamples";
            yield return "AppFrameExamplesCommon";
            yield return "AppFrameInHubTest";
            yield return "AppFrameTest";
            yield return "inHub";
            yield return "inHubMain";
            yield return "inHubNavireo";
            yield return "Main";
            yield return "MainNav";
        }

        public void Test()
        {
            lines.Clear();

            foreach (string vrlLine in FindCsProjs().Where(p => !GetDisabledProjects().Contains(Path.GetFileNameWithoutExtension(p))))
            {
                try
                {
                    Cl_NuspecCreator vrlNuspecCreator = new Cl_NuspecCreator(new Cl_AssemblyNameFinder(), new Cl_FileTextLoader(), new Cl_PackagesFinder(),
                        new Cl_ProjectReferencesFinder(), new Cl_NuspecXmlCreator(), new Cl_FileTextSaver(), new Cl_VersionFinder(new Cl_CsprojDllGetter()), new Cl_CsprojDllGetter());

                    string vrlNuspecFile = vrlNuspecCreator.CreateAndSaveNuspec(vrlLine);

                    if (!File.Exists(vrlNuspecFile))
                    {
                        throw new InvalidOperationException(vrlNuspecFile + ": Brak pliku NuSpec");
                    }

                    if (!File.Exists(Path.Combine(Path.GetDirectoryName(vrlNuspecFile), Path.GetFileNameWithoutExtension(vrlNuspecFile) + ".dll")))
                    {
                        throw new InvalidOperationException(vrlNuspecFile + ": Brak pliku dll");
                    }

                    if (!File.Exists(Path.Combine(Path.GetDirectoryName(vrlNuspecFile), Path.GetFileNameWithoutExtension(vrlNuspecFile) + ".xml")))
                    {
                        throw new InvalidOperationException(vrlNuspecFile + ": Brak pliku dokumentacji (xml)");
                    }

                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "nuget.exe",
                            Arguments = @"pack " + vrlNuspecFile + " -OutputDirectory " + frtxtOutputDirectory.Text,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    process.Start();
                    while (!process.StandardOutput.EndOfStream)
                    {
                        string line = process.StandardOutput.ReadLine();
                        lines.Add(line);
                        memoEdit1.Lines = lines.ToArray();
                    }
                    process.Dispose();
                }
                catch (Exception vrlException)
                {
                    XtraMessageBox.Show(vrlException.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Test();
        }
    }
}

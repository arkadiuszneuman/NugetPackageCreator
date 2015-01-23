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
using NugetTest.NuspecCreator.CsprojFileFinder;
using NugetTest.NuspecCreator.CsprojFileFinder.AssemblyNameFinder;
using NugetTest.NuspecCreator.CsprojFileFinder.PackagesFinder;
using NugetTest.NuspecCreator.CsprojFileFinder.ProjectReferences;
using NugetTest.NuspecCreator.CsprojFileFinder.VersionFinder;
using NugetTest.NuspecCreator.FileTextLoader;
using NugetTest.NuspecCreator.FileTextSaver;
using NugetTest.NuspecCreator.NuspecXmlCreator;

namespace NugetTest
{
    public partial class Frm_MainForm : Form, I_PreleaseInfo
    {

        public Frm_MainForm()
        {
            InitializeComponent();

            DateTime vrlDateTime = DateTime.Now;
            frtxtPreleaseNumber.Text = "v" + vrlDateTime.ToString("yyyyMMddHHmm");
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
            List<string> lines = new List<string>();

            foreach (string vrlLine in FindCsProjs().Where(p => !GetDisabledProjects().Contains(Path.GetFileNameWithoutExtension(p))))
            {
                string vrlNuspecFile = string.Empty;

                try
                {
                    Cl_NuspecCreator vrlNuspecCreator = new Cl_NuspecCreator(this, new Cl_AssemblyNameFinder(), new Cl_FileTextLoader(), new Cl_PackagesFinder(),
                        new Cl_ProjectReferencesFinder(), new Cl_NuspecXmlCreator(), new Cl_FileTextSaver(), new Cl_VersionFinder(new Cl_CsprojDllGetter()), new Cl_CsprojDllGetter());

                    vrlNuspecFile = vrlNuspecCreator.CreateAndSaveNuspec(vrlLine);

                    string vrlFileWithVersion = Path.GetFileNameWithoutExtension(vrlNuspecFile);
                    string vrlProjectName = Path.GetFileNameWithoutExtension(vrlLine);
                    string vrlVersion = vrlFileWithVersion.Replace(vrlProjectName + '.', "");

                    string vrlOutputDirectory = Path.Combine(frtxtOutputDirectory.Text, vrlVersion);
                    string vrlNuspecWithoutPrerelease = vrlNuspecFile;

                    if (IsPrerelease)
                    {
                        vrlNuspecWithoutPrerelease = vrlNuspecFile.Replace("-" + PrereleaseText, "");
                    }

                    if (!File.Exists(vrlNuspecFile))
                    {
                        throw new InvalidOperationException(vrlNuspecFile + ": Brak pliku NuSpec");
                    }

                    if (!File.Exists(Path.Combine(Path.GetDirectoryName(vrlNuspecWithoutPrerelease), Path.GetFileNameWithoutExtension(vrlNuspecWithoutPrerelease) + ".dll")))
                    {
                        throw new InvalidOperationException(vrlNuspecFile + ": Brak pliku dll");
                    }

                    if (!File.Exists(Path.Combine(Path.GetDirectoryName(vrlNuspecWithoutPrerelease), Path.GetFileNameWithoutExtension(vrlNuspecWithoutPrerelease) + ".xml")))
                    {
                        throw new InvalidOperationException(vrlNuspecFile + ": Brak pliku dokumentacji (xml)");
                    }

                    if (!Directory.Exists(vrlOutputDirectory))
                    {
                        Directory.CreateDirectory(vrlOutputDirectory);
                    }

                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "nuget.exe",
                            Arguments = @"pack " + vrlNuspecFile + " -OutputDirectory " + vrlOutputDirectory,
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
                finally
                {
                    if (File.Exists(vrlNuspecFile))
                    {
                        File.Delete(vrlNuspecFile);
                    }
                }
            }
        }

        private void ReleaseButtonClick(object sender, EventArgs e)
        {
            Test();
        }

        private void frbitPrerelease_CheckedChanged(object sender, EventArgs e)
        {
            frtxtPreleaseNumber.Enabled = frbitPrerelease.Checked;
        }

        public bool IsPrerelease
        {
            get { return frbitPrerelease.Checked; }
        }

        public string PrereleaseText
        {
            get { return frtxtPreleaseNumber.Text; }
        }
    }
}

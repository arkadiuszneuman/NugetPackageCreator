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

namespace NugetTest
{
    public partial class Form1 : Form
    {
        private List<string> lines = new List<string>();

        public Form1()
        {
            InitializeComponent();

        }

        private IEnumerable<string> FindCsProjs()
        {
            var vrlDir = Path.GetDirectoryName(frtxtSlnPath.Text);

            return Directory.GetFiles(vrlDir, "*.csproj", SearchOption.AllDirectories);
        }

        public void Test()
        {
            lines.Clear();

            foreach (string vrlLine in FindCsProjs())
            {


                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "nuget.exe",
                        Arguments = @"pack D:\Praca\inSolutions\Projekty\SzkieletAplikacji\ApplicationFrame-Evaluation\inSolutions.Utilities\bin\Release\inSolutions.Utilities.1.0.1.8.dll.nuspec -OutputDirectory " + frtxtOutputDirectory.Text,
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
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Test();
        }
    }
}

using System.Collections.Generic;
using System.IO;
using NugetTest.NuspecCreator.CsprojFileFinder;
using NugetTest.NuspecCreator.CsprojFileFinder.AssemblyNameFinder;
using NugetTest.NuspecCreator.CsprojFileFinder.PackagesFinder;
using NugetTest.NuspecCreator.CsprojFileFinder.ProjectReferences;
using NugetTest.NuspecCreator.CsprojFileFinder.VersionFinder;
using NugetTest.NuspecCreator.FileTextLoader;
using NugetTest.NuspecCreator.FileTextSaver;
using NugetTest.NuspecCreator.NuspecXmlCreator;

namespace NugetTest.NuspecCreator
{
    public class Cl_NuspecCreator
    {
        private readonly I_AssemblyNameFinder vrcAssemblyNameFinder;
        private readonly I_FileTextLoader vrcFileTextLoader;
        private readonly I_PackagesFinder vrcPackagesFinder;
        private readonly I_ProjectReferencesFinder vrcProjectReferencesFinder;
        private readonly I_NuspecXmlCreator vrcNuspecXmlCreator;
        private readonly I_FileTextSaver vrcFileTextSaver;
        private readonly I_VersionFinder vrcVersionFinder;

        public Cl_NuspecCreator(I_AssemblyNameFinder vrpAssemblyNameFinder, I_FileTextLoader vrpFileTextLoader, I_PackagesFinder vrpPackagesFinder,
            I_ProjectReferencesFinder vrpProjectReferencesFinder, I_NuspecXmlCreator vrpNuspecXmlCreator, I_FileTextSaver vrpFileTextSaver, I_VersionFinder vrpVersionFinder)
        {
            vrcAssemblyNameFinder = vrpAssemblyNameFinder;
            vrcFileTextLoader = vrpFileTextLoader;
            vrcPackagesFinder = vrpPackagesFinder;
            vrcProjectReferencesFinder = vrpProjectReferencesFinder;
            vrcNuspecXmlCreator = vrpNuspecXmlCreator;
            vrcFileTextSaver = vrpFileTextSaver;
            vrcVersionFinder = vrpVersionFinder;
        }

        public void CreateAndSaveNuspec(string vrpFilePath)
        {
            Cl_NuspecProjectInfo vrlNuspecProjectInfo = CreateNuspecProjectInfo(vrpFilePath);
            SaveNuspecFile(vrpFilePath, vrlNuspecProjectInfo);
        }

        private void SaveNuspecFile(string vrpFilePath, Cl_NuspecProjectInfo vrpNuspecProjectInfo)
        {
            string vrlFileName = Path.GetFileNameWithoutExtension(vrpFilePath) + ".nuspec";
            string vrlDir = Path.GetDirectoryName(vrpFilePath) + @"\bin\Release";

            string vrlNuspecText = vrcNuspecXmlCreator.CreateNuspecText(vrpNuspecProjectInfo);
            vrcFileTextSaver.SaveText(Path.Combine(vrlDir, vrlFileName), vrlNuspecText);
        }

        private Cl_NuspecProjectInfo CreateNuspecProjectInfo(string vrpFilePath)
        {
            string vrlTextFromFile = vrcFileTextLoader.GetTextFromFile(vrpFilePath);

            string vrlAssemblyName = vrcAssemblyNameFinder.GetAssemblyName(vrlTextFromFile);
            string vrlVersion = vrcVersionFinder.GetVersion(vrpFilePath, vrlAssemblyName);
            vrlAssemblyName = vrlAssemblyName.Replace('.' + vrlVersion, "");
            IEnumerable<Cl_ProjectInfo> vrlPackages = vrcPackagesFinder.GetPackages(vrlTextFromFile);
            IEnumerable<Cl_ProjectInfo> vrlProjectReferences = vrcProjectReferencesFinder.GetProjectReferences(vrlTextFromFile);

            return new Cl_NuspecProjectInfo()
            {
                ApplicationId = vrlAssemblyName,
                Version = vrlVersion,
                Authors = "Arkadiusz Neuman",
                Description = "Szkielet Aplikacji dla programów firmy inSolutions.",
                Title = "Szkielet Aplikacji - " + vrlAssemblyName.Replace("inSolutions.", ""),
                ProjectReferences = vrlProjectReferences,
                AddionalPackages = vrlPackages
            };
        }
    }
}
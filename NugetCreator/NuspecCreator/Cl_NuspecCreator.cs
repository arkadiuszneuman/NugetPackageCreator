using System.Collections.Generic;
using System.IO;
using System.Linq;
using NugetTest.CsprojDllGetter;
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
        private readonly I_PreleaseInfo vrcPreleaseInfo;
        private readonly I_AssemblyNameFinder vrcAssemblyNameFinder;
        private readonly I_FileTextLoader vrcFileTextLoader;
        private readonly I_PackagesFinder vrcPackagesFinder;
        private readonly I_ProjectReferencesFinder vrcProjectReferencesFinder;
        private readonly I_NuspecXmlCreator vrcNuspecXmlCreator;
        private readonly I_FileTextSaver vrcFileTextSaver;
        private readonly I_VersionFinder vrcVersionFinder;
        private readonly I_CsprojDllGetter vrcCsprojDllGetter;

        public Cl_NuspecCreator(I_PreleaseInfo vrpPreleaseInfo, I_AssemblyNameFinder vrpAssemblyNameFinder, I_FileTextLoader vrpFileTextLoader, I_PackagesFinder vrpPackagesFinder,
            I_ProjectReferencesFinder vrpProjectReferencesFinder, I_NuspecXmlCreator vrpNuspecXmlCreator, I_FileTextSaver vrpFileTextSaver, I_VersionFinder vrpVersionFinder, I_CsprojDllGetter vrpCsprojDllGetter)
        {
            vrcPreleaseInfo = vrpPreleaseInfo;
            vrcAssemblyNameFinder = vrpAssemblyNameFinder;
            vrcFileTextLoader = vrpFileTextLoader;
            vrcPackagesFinder = vrpPackagesFinder;
            vrcProjectReferencesFinder = vrpProjectReferencesFinder;
            vrcNuspecXmlCreator = vrpNuspecXmlCreator;
            vrcFileTextSaver = vrpFileTextSaver;
            vrcVersionFinder = vrpVersionFinder;
            vrcCsprojDllGetter = vrpCsprojDllGetter;
        }

        public string CreateAndSaveNuspec(string vrpFilePath)
        {
            Cl_NuspecProjectInfo vrlNuspecProjectInfo = CreateNuspecProjectInfo(vrpFilePath);
            return SaveNuspecFile(vrpFilePath, vrlNuspecProjectInfo);
        }

        private string SaveNuspecFile(string vrpFilePath, Cl_NuspecProjectInfo vrpNuspecProjectInfo)
        {
            string vrlDllDirectory = vrcCsprojDllGetter.GetDllDirectoryFromCsproj(vrpFilePath);
            string vrlFileName = Path.Combine(vrlDllDirectory, Path.GetFileNameWithoutExtension(vrpFilePath) + "." + vrpNuspecProjectInfo.NugetVersion + ".nuspec");

            string vrlNuspecText = vrcNuspecXmlCreator.CreateNuspecText(vrpNuspecProjectInfo);
            vrcFileTextSaver.SaveText(vrlFileName, vrlNuspecText);

            return vrlFileName;
        }

        private Cl_NuspecProjectInfo CreateNuspecProjectInfo(string vrpFilePath)
        {
            string vrlTextFromFile = vrcFileTextLoader.GetTextFromFile(vrpFilePath);

            string vrlAssemblyName = vrcAssemblyNameFinder.GetAssemblyName(vrlTextFromFile);
            string vrlVersion = vrcVersionFinder.GetVersion(vrpFilePath, vrlAssemblyName);
            vrlAssemblyName = vrlAssemblyName.Replace('.' + vrlVersion, "");
            IEnumerable<Cl_ProjectInfo> vrlPackages = vrcPackagesFinder.GetPackages(vrlTextFromFile).ToList();
            IEnumerable<Cl_ProjectInfo> vrlProjectReferences = vrcProjectReferencesFinder.GetProjectReferences(vrlTextFromFile).ToList();

            string vrlFullVersionName = vrlVersion;

            if (vrcPreleaseInfo.IsPrerelease)
            {
                vrlFullVersionName += "-" + vrcPreleaseInfo.PrereleaseText;
            }

            foreach (Cl_ProjectInfo vrlProjectReference in vrlProjectReferences)
            {
                vrlProjectReference.Version = '[' + vrlFullVersionName + ']'; //ustawienie wersji programu dokładnie na taką samą jak wersja aktualnej dll'ki
            }

            return new Cl_NuspecProjectInfo()
            {
                ApplicationId = vrlAssemblyName,
                NugetVersion = vrlFullVersionName,
                FileVersion = vrlVersion,
                Authors = "Arkadiusz Neuman",
                Description = "Szkielet Aplikacji dla programów firmy inSolutions.",
                Title = "Szkielet Aplikacji - " + vrlAssemblyName.Replace("inSolutions.", ""),
                ProjectReferences = vrlProjectReferences,
                AddionalPackages = vrlPackages
            };
        }
    }
}
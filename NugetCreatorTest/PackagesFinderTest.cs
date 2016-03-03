using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NugetTest.NuspecCreator.CsprojFileFinder;
using NugetTest.NuspecCreator.CsprojFileFinder.PackagesFinder;

namespace NugetCreatorTest
{
    [TestClass]
    public class PackagesFinderTest
    {
        private IEnumerable<Cl_ProjectInfo> GetPackages()
        {
            Cl_PackagesFinder vrlPackagesFinder = new Cl_PackagesFinder();
            return vrlPackagesFinder.GetPackages(new TextGetter().GetCsprojText());
        }

        private Cl_ProjectInfo GetProjectInfo(string vrpProjectInfoName)
        {
            IEnumerable<Cl_ProjectInfo> vrlPackages = GetPackages();

            Cl_ProjectInfo vrlProjectInfo = vrlPackages.SingleOrDefault(p => p.Name == vrpProjectInfoName);
            if (vrlProjectInfo == null)
            {
                Assert.Fail("Nie znaleziono modułu " + vrpProjectInfoName);
            }

            return vrlProjectInfo;
        }

        [TestMethod]
        public void Check_Packages_Quantity()
        {
            IEnumerable<Cl_ProjectInfo> vrlPackages = GetPackages();

            Assert.AreEqual(3, vrlPackages.Count());
        }

        [TestMethod]
        public void Check_Existance_Of_Package_FluentMigrator()
        {
            Cl_ProjectInfo vrlProjectInfo = GetProjectInfo("FluentMigrator");
            Assert.IsNotNull(vrlProjectInfo);
        }

        [TestMethod]
        public void Check_Existance_Of_Package_FluentMigratorRunner()
        {
            Cl_ProjectInfo vrlProjectInfo = GetProjectInfo("FluentMigrator.Runner");
            Assert.IsNotNull(vrlProjectInfo);
        }

        [TestMethod]
        public void Check_Existance_Of_Package_NLog()
        {
            Cl_ProjectInfo vrlProjectInfo = GetProjectInfo("NLog");
            Assert.IsNotNull(vrlProjectInfo);
        }

        [TestMethod]
        public void Check_Version_Of_Package_FluentMigrator()
        {
            Cl_ProjectInfo vrlProjectInfo = GetProjectInfo("FluentMigrator");

            Assert.AreEqual("1.4.0.0", vrlProjectInfo.Version);
        }

        [TestMethod]
        public void Check_Version_Of_Package_FluentMigratorRunner()
        {
            Cl_ProjectInfo vrlProjectInfo = GetProjectInfo("FluentMigrator.Runner");

            Assert.AreEqual("1.4.0.0", vrlProjectInfo.Version);
        }

        [TestMethod]
        public void Check_Version_Of_Package_NLog()
        {
            Cl_ProjectInfo vrlProjectInfo = GetProjectInfo("NLog");

            Assert.AreEqual("3.2.0.0", vrlProjectInfo.Version);
        }
    }
}
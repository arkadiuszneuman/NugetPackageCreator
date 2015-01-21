using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NugetTest.NuspecCreator.CsprojFileFinder;
using NugetTest.NuspecCreator.CsprojFileFinder.ProjectReferences;

namespace NugetCreatorTest
{
    [TestClass]
    public class ProjectReferencesTest
    {
        private IEnumerable<Cl_ProjectInfo> GetProjectReferences()
        {
            Cl_ProjectReferencesFinder vrlProjectReferencesFinder = new Cl_ProjectReferencesFinder();
            return vrlProjectReferencesFinder.GetProjectReferences(new TextGetter().GetCsprojText());
        }

        [TestMethod]
        public void Check_Packages_Quantity()
        {
            IEnumerable<Cl_ProjectInfo> vrlPackages = GetProjectReferences();

            Assert.AreEqual(2, vrlPackages.Count());
        }

        [TestMethod]
        public void Check_Package_SystemInterfaces()
        {
            IEnumerable<Cl_ProjectInfo> vrlPackages = GetProjectReferences();

            Assert.IsTrue(vrlPackages.Any(p => p.Name == "inSolutions.SystemInterfaces"));
        }

        [TestMethod]
        public void Check_Package_Nexo()
        {
            IEnumerable<Cl_ProjectInfo> vrlPackages = GetProjectReferences();

            Assert.IsTrue(vrlPackages.Any(p => p.Name == "inSolutions.Nexo"));
        }
    }
}
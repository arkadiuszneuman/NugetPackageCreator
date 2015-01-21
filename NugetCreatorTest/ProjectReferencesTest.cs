using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NugetTest.CsprojFileFinder;
using NugetTest.CsprojFileFinder.ProjectReferences;

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

            Assert.AreEqual(1, vrlPackages.Count());
        }

        [TestMethod]
        public void Check_Package_ApplicationFrameSystemInterfaces()
        {
            IEnumerable<Cl_ProjectInfo> vrlPackages = GetProjectReferences();

            Assert.IsTrue(vrlPackages.Any(p => p.Name == "ApplicationFrameSystemInterfaces"));
        }
    }
}
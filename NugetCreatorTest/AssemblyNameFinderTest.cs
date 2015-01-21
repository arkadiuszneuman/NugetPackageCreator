using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NugetTest.CsprojFileFinder.DependencyFinder;
using NugetTest.CsprojFileFinder.FileTextFinder;

namespace NugetCreatorTest
{
    [TestClass]
    public class AssemblyNameFinderTest
    {
        [TestMethod]
        public void Is_Valid_Assembly_Name()
        {
            Cl_AssemblyNameFinder vrlAssemblyNameFinder = new Cl_AssemblyNameFinder();

            string vrlAssemblyName = vrlAssemblyNameFinder.GetAssemblyName(new TextGetter().GetCsprojText());

            Assert.AreEqual("inSolutions.Utilities.1.0.1.8", vrlAssemblyName);
        }
    }
}

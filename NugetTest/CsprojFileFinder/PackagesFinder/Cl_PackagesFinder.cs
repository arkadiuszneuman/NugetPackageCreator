﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace NugetTest.CsprojFileFinder.PackagesFinder
{
    public class Cl_Package
    {
        public Cl_Package(string vrpName)
        {
            Name = vrpName;
        }

        public string Name { get; set; }
    }

    public class Cl_PackagesFinder
    {
        public IEnumerable<Cl_Package> GetPackages(string vrpCsprojText)
        {
            XElement vrlRoot = XElement.Load(new StringReader(vrpCsprojText));
            foreach (XElement vrlReferenceInclude in vrlRoot.Elements().First(el => el.Name.LocalName == "ItemGroup").Elements().Where(r => r.Name.LocalName == "Reference" && r.Elements().Select(el => el.Name.LocalName).Contains("HintPath")))
            {
                yield return new Cl_Package(vrlReferenceInclude.Attribute("Include").Value.Split(',').First());
            }
        }
    }
}
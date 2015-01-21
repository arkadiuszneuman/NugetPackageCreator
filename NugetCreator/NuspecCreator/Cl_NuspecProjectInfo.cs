﻿using System.Collections;
using System.Collections.Generic;
using NugetTest.CsprojFileFinder;

namespace NugetTest.NuspecCreator
{
    public class Cl_NuspecProjectInfo
    {
        public string ApplicationId { get; set; }
        public string Version { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }

        public IEnumerable<Cl_ProjectInfo> ProjectReferences { get; set; }
        public IEnumerable<Cl_ProjectInfo> AddionalPackages { get; set; }
    }
}
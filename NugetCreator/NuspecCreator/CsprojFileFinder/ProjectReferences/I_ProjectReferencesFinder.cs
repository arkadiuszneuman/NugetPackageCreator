using System.Collections.Generic;

namespace NugetTest.NuspecCreator.CsprojFileFinder.ProjectReferences
{
    public interface I_ProjectReferencesFinder
    {
        IEnumerable<Cl_ProjectInfo> GetProjectReferences(string vrpCsprojText);
    }
}
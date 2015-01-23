namespace NugetTest.NuspecCreator.CsprojFileFinder
{
    public interface I_PreleaseInfo
    {
        bool IsPrerelease { get; }
        string PrereleaseText { get; }
    }
}
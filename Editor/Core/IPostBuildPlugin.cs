using UnityEditor.Build.Reporting;

public interface IPostBuildPlugin : IBuildPlugin
{
    void OnPostBuild(BuildReport report);
}
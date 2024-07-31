using UnityEditor;

public interface IPreBuildPlugin : IBuildPlugin
{
    void OnPreBuild(BuildTarget target);
}
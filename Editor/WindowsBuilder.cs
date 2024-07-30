using System.IO;
using UnityEditor;

public class WindowsBuilder : Builder
{
    protected override string PlatformName => "Windows";
    protected override BuildTarget BuildTarget => BuildTarget.StandaloneWindows64;
    protected override string FileExtension => ".exe";

    [MenuItem("Build/Windows")]
    public static void BuildWindows()
    {
        new WindowsBuilder().Build();
    }

    protected override string GetBuildPath(string buildName)
    {
        string projectName = PlayerSettings.productName;
        return Path.Combine("Builds", PlatformName, buildName, $"{projectName}{FileExtension}");
    }
}

using UnityEditor;
using UnityEditor.Build.Reporting;

public class AndroidBuilder : Builder
{
    protected override string PlatformName => "Android";
    protected override BuildTarget BuildTarget => BuildTarget.Android;
    protected override string FileExtension => ".apk";

    [MenuItem("Build/Android/Build")]
    public static void BuildAndroid()
    {
        new AndroidBuilder().Build();
    }

    [MenuItem("Build/Android/Build and Run")]
    public static void BuildAndRunAndroid()
    {
        new AndroidBuilder().BuildAndRun();
    }

    private void BuildAndRun()
    {
        string buildName = GenerateBuildName();
        string buildPath = GetBuildPath(buildName);

        PreBuildActions(buildPath);

        BuildPlayerOptions buildPlayerOptions = GetBuildPlayerOptions(buildPath);
        buildPlayerOptions.options |= BuildOptions.AutoRunPlayer;

        BuildReport report = ExecuteBuild(buildPlayerOptions);

        PostBuildActions(report);
    }
}
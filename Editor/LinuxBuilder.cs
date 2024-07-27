using UnityEditor;

public class LinuxBuilder : Builder
{
    protected override string PlatformName => "Linux";
    protected override BuildTarget BuildTarget => BuildTarget.StandaloneLinux64;

    [MenuItem("Build/Linux")]
    public static void BuildLinux()
    {
        new LinuxBuilder().Build();
    }
}

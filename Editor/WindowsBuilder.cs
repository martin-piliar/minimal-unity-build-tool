using UnityEditor;

public class WindowsBuilder : Builder
{
    protected override string PlatformName => "Windows";
    protected override BuildTarget BuildTarget => BuildTarget.StandaloneWindows64;

    [MenuItem("Build/Windows")]
    public static void BuildWindows()
    {
        new WindowsBuilder().Build();
    }
}

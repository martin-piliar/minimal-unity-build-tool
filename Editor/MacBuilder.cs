using UnityEditor;

public class MacBuilder : Builder
{
    protected override string PlatformName => "MacOS";
    protected override BuildTarget BuildTarget => BuildTarget.StandaloneOSX;

    [MenuItem("Build/MacOS")]
    public static void BuildMac()
    {
        new MacBuilder().Build();
    }
}

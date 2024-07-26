using UnityEditor;

public class IOSBuilder : Builder
{
    protected override string PlatformName => "iOS";
    protected override BuildTarget BuildTarget => BuildTarget.iOS;

    [MenuItem("Build/iOS")]
    public static void BuildIOS()
    {
        new IOSBuilder().Build();
    }
}

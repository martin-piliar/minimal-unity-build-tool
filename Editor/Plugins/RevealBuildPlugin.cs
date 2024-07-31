using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;

public class RevealBuildPlugin : IPostBuildPlugin
{
    public string Name => "Reveal Build in Finder";

    public void OnPostBuild(BuildReport report)
    {
        RevealInFinder(report.summary.outputPath);
    }
    
    private static void RevealInFinder(string path)
    {
        if (File.Exists(path))
        {
            EditorUtility.RevealInFinder(path);
        }
        else if (Directory.Exists(path))
        {
            EditorUtility.RevealInFinder(path);
        }
        else
        {
            throw new FileNotFoundException($"File or directory not found: {path}");
        }
    }
}

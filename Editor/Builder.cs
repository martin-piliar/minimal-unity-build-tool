using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public abstract class Builder
{
    protected abstract string PlatformName { get; }
    protected abstract BuildTarget BuildTarget { get; }
    protected virtual string FileExtension => string.Empty;

    protected virtual string GenerateBuildName()
    {
        string projectName = PlayerSettings.productName;
        string dateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        return $"{projectName}_{dateTime}";
    }

    protected virtual string GetBuildPath(string buildName)
    {
        return Path.Combine("Builds", PlatformName, buildName + FileExtension);
    }

    protected virtual BuildPlayerOptions GetBuildPlayerOptions(string buildPath)
    {
        return new BuildPlayerOptions
        {
            scenes = GetEnabledScenes(),
            locationPathName = buildPath,
            target = BuildTarget,
            options = BuildOptions.None
        };
    }

    protected virtual string[] GetEnabledScenes()
    {
        return EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();
    }

    protected virtual void PreBuildSetup(string buildPath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(buildPath) ?? string.Empty);
    }

    protected virtual void PostBuildActions(BuildReport report)
    {
        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"Build succeeded: {report.summary.outputPath}");
        }
        else
        {
            Debug.LogError($"Build failed for {PlatformName}");
        }
    }

    protected virtual BuildReport ExecuteBuild(BuildPlayerOptions options)
    {
        return BuildPipeline.BuildPlayer(options);
    }

    protected virtual void Build()
    {
        string buildName = GenerateBuildName();
        string buildPath = GetBuildPath(buildName);

        PreBuildSetup(buildPath);

        BuildPlayerOptions buildPlayerOptions = GetBuildPlayerOptions(buildPath);

        BuildReport report = ExecuteBuild(buildPlayerOptions);

        PostBuildActions(report);
    }
}
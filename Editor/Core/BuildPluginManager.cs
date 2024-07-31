using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class BuildPluginManager
{
    private static readonly List<IPreBuildPlugin> PreBuildPlugins = new();
    private static readonly List<IPostBuildPlugin> PostBuildPlugins = new();
    
    private static void LoadPlugins()
    {
        PostBuildPlugins.Clear();
        PreBuildPlugins.Clear();
        
        var pluginTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IBuildPlugin).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

        foreach (var pluginType in pluginTypes)
        {
            var plugin = (IBuildPlugin)Activator.CreateInstance(pluginType);
            RegisterPlugin(plugin);
        }
    }

    private static void RegisterPlugin(IBuildPlugin plugin)
    {
        switch (plugin)
        {
            case IPreBuildPlugin preBuildPlugin:
                PreBuildPlugins.Add(preBuildPlugin);
                Debug.Log($"Registered pre-build plugin: {preBuildPlugin.Name}");
                break;
            case IPostBuildPlugin postBuildPlugin:
                PostBuildPlugins.Add(postBuildPlugin);
                Debug.Log($"Registered post-build plugin: {postBuildPlugin.Name}");
                break;
        }
    }

    public static void UnregisterPlugin(IBuildPlugin plugin)
    {
        switch (plugin)
        {
            case IPreBuildPlugin preBuildPlugin:
                PreBuildPlugins.Remove(preBuildPlugin);
                break;
            case IPostBuildPlugin postBuildPlugin:
                PostBuildPlugins.Remove(postBuildPlugin);
                break;
        }

        Debug.Log($"Unregistered build plugin: {plugin.Name}");
    }

    public static void ExecutePreBuild(BuildTarget target)
    {
        LoadPlugins();
        
        Debug.Log($"Executing [{PreBuildPlugins.Count}] pre-build plugins ");
        foreach (var plugin in PreBuildPlugins)
        {
            try
            {
                plugin.OnPreBuild(target);
                Debug.Log($"Executed pre-build plugin: {plugin.Name}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in pre-build plugin {plugin.Name}: {e.Message}");
            }
        }
    }

    public static void ExecutePostBuild(BuildReport report)
    {
        Debug.Log($"Executing [{PostBuildPlugins.Count}] post-build plugins ");
        foreach (var plugin in PostBuildPlugins)
        {
            try
            {
                plugin.OnPostBuild(report);
                Debug.Log($"Executed post-build plugin: {plugin.Name}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in post-build plugin {plugin.Name}: {e.Message}");
            }
        }
    }
}
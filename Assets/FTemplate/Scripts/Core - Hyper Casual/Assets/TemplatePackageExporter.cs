using UnityEditor;
using System.Collections.Generic;

#if UNITY_EDITOR
public static class TemplatePackageExporter
{
    private static readonly string ExtensionsPath = "Assets/FTemplate/Extensions";

    private static readonly List<string> AlwaysNotInclude = new List<string>
    {
        "Assets/FTemplate/Scripts/Core/Assets/TemplatePackageExporter.cs",
        "Assets/FTemplate/_Demo",
    };

    private static readonly List<string> ExtensionPaths = new List<string>
    {
        "Assets/FTemplate/Extensions/Minigames",
        "Assets/FTemplate/Extensions/Runner",
    };

    [MenuItem("Flamingo/Exporter/Normal Template")]
    public static void ExportNormalTemplate()
    {
        var pathList = AlwaysNotInclude;
        pathList.Add(ExtensionsPath);

        ExportAsPackage(pathList, "Normal Template");
    }

    [MenuItem("Flamingo/Exporter/Minigames Template")]
    public static void ExportMinigamesTemplate()
    {
        var pathList = ExtensionPaths;
        pathList.Remove("Assets/FTemplate/Extensions/Minigames");
        pathList.AddRange(AlwaysNotInclude);

        ExportAsPackage(pathList, "Minigames Template");
    }

    [MenuItem("Flamingo/Exporter/Runner Template")]
    public static void ExportRunnerTemplate()
    {
        var pathList = ExtensionPaths;
        pathList.Remove("Assets/FTemplate/Extensions/Runner");
        pathList.AddRange(AlwaysNotInclude);


        ExportAsPackage(pathList, "Runner Template");
    }

    public static void ExportAsPackage(List<string> notIncludedThisPaths, string templateName)
    {
        var paths = AssetDatabase.GetAllAssetPaths();
        var pathsList = new List<string>();

        for (int i = 0; i < paths.Length; i++)
        {
            bool include = true;
            notIncludedThisPaths.ForEach(x => { if (paths[i].StartsWith(x)) include = false; });
            if (include) pathsList.Add(paths[i]);
        }

        AssetDatabase.ExportPackage(
            pathsList.ToArray(),
            templateName + "_" + PlayerSettings.productName + ".unitypackage",
            ExportPackageOptions.Interactive | ExportPackageOptions.IncludeLibraryAssets
        );
    }
}

#endif
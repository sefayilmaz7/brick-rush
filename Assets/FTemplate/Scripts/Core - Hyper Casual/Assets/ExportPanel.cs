using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public static class ExporterConstants
{
    public static string EXTENSIONS_PATH = "Assets/FTemplate/Extensions";
    public static string SYSTEMS_PATH = "Assets/FTemplate/Scripts/Systems";
}

public class ExportPanel : EditorWindow
{
    private bool extensionsEnabled = false;
    private bool systemsEnabled = false;

    private FExtension runner;
    private bool runnerEnabled = false;

    private FExtension minigames;
    private bool minigamesEnabled = false;

    private FExtension meshSlicer;
    private bool slicerEnabled = false;

    private FSystem shooter;
    private bool shooterEnabled = false;

    private bool setup = false;
    private bool exporting = false;

    [MenuItem("Flamingo/Export Window")]
    static void Init()
    {
        ExportPanel window = (ExportPanel)EditorWindow.GetWindow(typeof(ExportPanel));
        window.Show();
    }

    void OnGUI()
    {
        if (exporting) return;

        if (!setup) Execute();

        GUILayout.Label("Flamingo Exporter", EditorStyles.boldLabel);

        extensionsEnabled = EditorGUILayout.BeginToggleGroup("Extensions", extensionsEnabled);
        runnerEnabled = EditorGUILayout.Toggle(runner.name, runnerEnabled);
        minigamesEnabled = EditorGUILayout.Toggle(minigames.name, minigamesEnabled);
        slicerEnabled = EditorGUILayout.Toggle(meshSlicer.name, slicerEnabled);
        EditorGUILayout.EndToggleGroup();

        systemsEnabled = EditorGUILayout.BeginToggleGroup("Systems", systemsEnabled);
        shooterEnabled = EditorGUILayout.Toggle(shooter.name, shooterEnabled);
        EditorGUILayout.EndToggleGroup();

        exporting = GUILayout.Button("Export");

        if (exporting) 
            DoExport();
    }

    public void Execute()
    {
        runner = new FExtension(name: "Runner", path: "Runner");
        minigames = new FExtension(name: "Mini Games", path: "Minigames");
        meshSlicer = new FExtension(name: "Slicer", path: "Slicer");

        shooter = new FSystem(name: "Shooter", path: "Shooter");
    }

    private void DoExport()
    {
        exporting = false;

        var excluded = new List<string>();

        if (!runnerEnabled) excluded.Add(runner.ProjectPath);
        if (!minigamesEnabled) excluded.Add(minigames.ProjectPath);
        if (!slicerEnabled) excluded.Add(meshSlicer.ProjectPath);

        if (!shooterEnabled) excluded.Add(shooter.ProjectPath);

        ExportAsPackage(excluded, "FTemplate_Package");
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


public struct FExtension
{
    public string name;
    public string path;

    public string ProjectPath => Path.Combine(ExporterConstants.EXTENSIONS_PATH, path);

    public FExtension(string name, string path)
    {
        this.name = name;
        this.path = path;
    }
}

public struct FSystem
{
    public string name;
    public string path;

    public string ProjectPath => Path.Combine(ExporterConstants.SYSTEMS_PATH, path);

    public FSystem(string name, string path)
    {
        this.name = name;
        this.path = path;
    }
}

#endif
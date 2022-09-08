using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

public static class GameSetup
{
    [MenuItem("Flamingo/Game/Create Common Folders")]
    public static void CreateFolders()
    {
        var folders = new List<string> { "Animations", "Textures", "Materials", "Prefabs", "Models", "Scripts" };

        if (!AssetDatabase.IsValidFolder("Assets/Game"))
            AssetDatabase.CreateFolder("Assets", "Game");

        foreach (var folder in folders)
        {
            if (!AssetDatabase.IsValidFolder("Assets/Game/" + folder))
                AssetDatabase.CreateFolder("Assets/Game", folder);
        }
    }
}

#endif
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class AssetImporter
{
    static AssetImporter()
    {
        EditorApplication.projectWindowChanged += OnProjectChanged;
        // https://docs.unity3d.com/ScriptReference/EditorWindow.OnProjectChange.html
        // According to Unity this code is not obsolete
    }

    static void OnProjectChanged()
    {
        DirectoryInitializer.InitializeDirectories();
        Debug.Log("OnProjectChanged");
    }

}

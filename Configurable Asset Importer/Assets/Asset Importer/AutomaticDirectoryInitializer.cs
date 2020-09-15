using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class AutomaticDirectoryInitializer : EditorWindow
{
    static AutomaticDirectoryInitializer()
    {
        EditorApplication.projectChanged += OnProjectChanged;
    }

    static void OnProjectChanged()
    {
        DirectoryInitializer.InitializeDirectories(false);

        AssetDatabase.Refresh(ImportAssetOptions.DontDownloadFromCacheServer);
    }
}

using UnityEditor;
using UnityEngine;


namespace BitGames.CustomAssetImporter
{
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

            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
        }
    }
}
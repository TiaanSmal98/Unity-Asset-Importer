using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

public class IgnorePaths : EditorWindow
{
    [MenuItem("Asset Importer/Configure/Ignore Paths")]
    public static void IgnorePathsFilePicker()
    {
        string excludedPath = EditorUtility.OpenFolderPanel("Select a folder to ignore", "Assets", "");
        DirectoryInitializer.MarkDirectoryAsIgnored(excludedPath);
    }
}

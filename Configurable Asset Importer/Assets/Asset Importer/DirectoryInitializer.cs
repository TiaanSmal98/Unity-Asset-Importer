using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public static class DirectoryInitializer
{
    /// <summary>
    /// The name of the Json config file created in directories
    /// </summary>
    public static string JsonConfigFileName = "ImportSettings.json";

    /// <summary>
    /// Initializes all directories within the Unity assets directory with a configuration file
    /// </summary>
    public static void Start()
    {
        List<string> directories = FindAllAssetDirectories("Assets"); //Find all directories within the Unity assets directory
        directories.Add("Assets");


        foreach (var currentPath in directories)
        {
            if (!ContainsValidConfigurationFile(currentPath))
            {
                CreateAssetConfigFile(currentPath);
            }
        }

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// Scans a directory for a configuration file
    /// </summary>
    /// <param name="path">The path to scan</param>
    /// <returns>True if the directory contains a configuration file, false if it's not found</returns>
    private static bool ContainsValidConfigurationFile(string path)
    {
        var filesInDirectory = Directory.GetFiles(path);

        foreach (var file in filesInDirectory)
        {
            if (Path.GetFileName(file) == Path.GetFileName(JsonConfigFileName))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Creates an empty Asset configuration file
    /// </summary>
    /// <param name="path">The directory in which to create a json config file</param>
    private static void CreateAssetConfigFile(string path)
    {
        string json = JsonUtility.ToJson(new ImportSettings());
        HelperFunctions.CreateText(path, JsonConfigFileName, json);
    }

    /// <summary>
    /// Finds all directories within the Unity project asset folder
    /// </summary>
    /// <returns>A list of Unity Directories</returns>
    private static List<string> FindAllAssetDirectories(string path)
    {
        List<string> parentDirectories = new List<string>();
        List<string> directories = new List<string>();

        parentDirectories.AddRange(HelperFunctions.ArrayToList<string>(AssetDatabase.GetSubFolders(path)));

        directories.AddRange(parentDirectories);

        foreach (var currentDirectory in parentDirectories)
        {
            directories.AddRange(FindAllAssetDirectories(currentDirectory)); // Recursively finds all directories
        }

        return directories;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Reflection;

public static class DirectoryInitializer
{
    /// <summary>
    /// The name of the Json config file created in directories
    /// </summary>
    public static string JsonConfigFileName = "ImportSettings.json";

    public static string IgnoreDirectoryFileName = "AssetImporter.Ignore";

    /// <summary>
    /// The path of the root directory of the Unity assets folder
    /// </summary>
    public static string RootAssetDirectory = "Assets";

    /// <summary>
    /// Initializes all directories within the Unity assets directory with a configuration file
    /// </summary>
    public static void InitializeDirectories(bool resetToDefault)
    {
        List<string> directories = FindDirectories(RootAssetDirectory, true); //Find all directories within the Unity assets directory
        directories.Add(RootAssetDirectory);
        
        foreach (var currentPath in directories)
        {
            if (!ContainsValidConfigurationFile(currentPath) || resetToDefault)
            {
                HelperFunctions.CreateJsonFile(currentPath, JsonConfigFileName, new ImportSettings().Initialize());
            }
        }
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
    /// Finds all directories within the Unity project asset folder
    /// </summary>
    /// <param name="path">The path to search</param>
    /// <param name="searchChildren">Recursively search for all directories contained in the directory, and its children</param>
    /// <returns>A list of Directories within the given path</returns>
    public static List<string> FindDirectories(string path, bool searchChildren)
    {
        List<string> childDirectories = new List<string>();
        List<string> directories = new List<string>();

        childDirectories.AddRange(HelperFunctions.ArrayToList<string>(AssetDatabase.GetSubFolders(path)));

        directories.AddRange(childDirectories);

        if (searchChildren) // Recursively finds all directories
        {
            foreach (var currentDirectory in childDirectories)
            {
                if (!IsIgnoredPath(currentDirectory)) // ensures children of ignored directory aren't included
                {
                    directories.AddRange(FindDirectories(currentDirectory, searchChildren));
                }
            }
        }

        int i = 0;
        while (i < directories.Count) // removes ignored directories
        {
            if (IsIgnoredPath(directories[i]))
            {
                directories.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }

        return directories;
    }

    /// <summary>
    /// Determines if a particular directory should be ignored
    /// </summary>
    /// <param name="directoryPath">The directory to query</param>
    /// <returns>True if ignored, false if included</returns>
    private static bool IsIgnoredPath(string directoryPath)
    {
        string fullPath = Path.Combine(directoryPath, IgnoreDirectoryFileName);

        if (File.Exists(fullPath))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Marks a directory as an ignored directory 
    /// This also effects child directories, but does not individually mark them
    /// Configuration settings will remain in child directories, but won't be applied
    /// Configuration settings applied in the root of the path will be deleted
    /// </summary>
    /// <param name="path">The directory to ignore</param>
    public static void MarkDirectoryAsIgnored(string path)
    {
        string fullPath = Path.Combine(path, JsonConfigFileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        fullPath = Path.Combine(path, IgnoreDirectoryFileName);

        if (!File.Exists(fullPath))
        {
            File.Create(fullPath);
        }
    }

}

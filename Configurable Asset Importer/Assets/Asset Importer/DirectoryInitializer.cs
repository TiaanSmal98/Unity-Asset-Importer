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
            if (!ContainsValidConfigurationFile(currentPath))
            {
                HelperFunctions.CreateJsonFile(currentPath, JsonConfigFileName, new ImportSettings());
            }
        }
    }

    /// <summary>
    /// Loops through all directories in the project and processes all configuration files
    /// </summary>
    /// <param name="path">The root path to search in</param>
    /// <param name="defaultSettings">The settings the current directory is inheriting from</param>
    /// <returns>A directory structure with all app settings and directories represented</returns>
    public static DirectoryStructure GetImportSettings(string path, ImportSettings defaultSettings = null)
    {
        DirectoryStructure currentDirectory = new DirectoryStructure();

        currentDirectory.path = path;

        currentDirectory.settings = HelperFunctions.JsonToClass<ImportSettings>(path, RootAssetDirectory);

        currentDirectory.childSettings = new List<DirectoryStructure>();

        List<string> childDirectories = FindDirectories(path, false);

        foreach (var currentChild in childDirectories)
        {
            DirectoryStructure currentChildStructure = GetImportSettings(currentChild, currentDirectory.settings); // Reads the specified settings from JSON
            currentChildStructure.settings.InheritSettings(defaultSettings); // Applies default settings from parent directory JSON configuration file

            currentDirectory.childSettings.Add(currentChildStructure);
        }

        return currentDirectory;
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
    private static List<string> FindDirectories(string path, bool searchChildren)
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

    private static bool IsIgnoredPath(string directoryPath)
    {
        string fullPath = Path.Combine(directoryPath, IgnoreDirectoryFileName);

        if (File.Exists(fullPath))
        {
            return true;
        }

        return false;
    }

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

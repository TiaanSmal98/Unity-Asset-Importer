using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ApplySettings
{
    /// <summary>
    /// Imports all assets and applies relevant settings supplied in JSON
    /// </summary>
    public static void ImportAndApplySettings()
    {
        DirectoryStructure directoryStructure = GetImportSettings(DirectoryInitializer.RootAssetDirectory);

        ApplySettingsRecursively(directoryStructure);
    }

    private static void ApplySettingsRecursively(DirectoryStructure structure)
    {
        if (structure.settings.AndroidSettings != null
            && structure.settings.AndroidSettings.OverrideForAndroid != null
            && structure.settings.AndroidSettings.OverrideForAndroid == true)
        {
            ApplyAndroidSettings(structure.settings.AndroidSettings, structure.path);
        }
        else
        {
            ApplyUniversalSettings(structure.settings.UniversalSettings, structure.path);
        }

        foreach (var childStructure in structure.childSettings)
        {
            ApplySettingsRecursively(childStructure);
        }

    }

    private static void ApplyUniversalSettings(UniversalSettings settings, string path)
    {
        if (settings.AudioCompressionFormat != null)
        {
            
        }
        if (settings.AudioLoadType != null)
        {

        }
        if (settings.AudioSampleRate != null)
        {

        }
        if (settings.MaxMipMapLevel != null)
        {

        }
        if (settings.MaxTextureSize != null)
        {

        }
    }

    private static void ApplyAndroidSettings(AndroidSettings settings, string path)
    {
        ApplyUniversalSettings((UniversalSettings)settings, path);
        // custom android settings that are not part of Universal Settings can be added here later
    }

    /// <summary>
    /// Loops through all directories in the project and processes all configuration files
    /// </summary>
    /// <param name="path">The root path to search in</param>
    /// <param name="defaultSettings">The settings the current directory is inheriting from. Use this to programatically assign default settings</param>
    /// <returns>A directory structure with all app settings and directories represented</returns>
    private static DirectoryStructure GetImportSettings(string path, ImportSettings defaultSettings = null)
    {
        DirectoryStructure currentDirectory = new DirectoryStructure();

        currentDirectory.path = path;

        currentDirectory.settings = HelperFunctions.JsonToClass<ImportSettings>(path, DirectoryInitializer.RootAssetDirectory);

        currentDirectory.childSettings = new List<DirectoryStructure>();

        List<string> childDirectories = DirectoryInitializer.FindDirectories(path, false);

        foreach (var currentChild in childDirectories)
        {
            DirectoryStructure currentChildStructure = GetImportSettings(currentChild, currentDirectory.settings); // Reads the specified settings from JSON
            currentChildStructure.settings.InheritSettings(defaultSettings); // Applies default settings from parent directory JSON configuration file

            currentDirectory.childSettings.Add(currentChildStructure);
        }

        return currentDirectory;
    }
}

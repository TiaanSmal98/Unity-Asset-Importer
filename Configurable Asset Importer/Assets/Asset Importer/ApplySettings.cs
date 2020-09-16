using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Presets;
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
        if (structure.settings.AndroidSettings.OverrideForAndroid == 1 &&  EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
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

    private static void ApplyAndroidSettings(AndroidSettings settings, string path)
    {
        ApplyUniversalSettings((UniversalSettings)settings, path);
        // custom android settings that are not part of Universal Settings can be added here later
    }

    private static void ApplyUniversalSettings(UniversalSettings settings, string path)
    {
        if (settings.AudioCompressionFormat > 0)
        {
            ApplyAudioCompressionFormat(path, settings.AudioCompressionFormat);
        }
        if (settings.AudioLoadType > 0)
        {
            ApplyAudioLoadType(path, settings.AudioLoadType);
        }
        if (settings.AudioSampleRate > 0)
        {
            ApplyAudioSampleRate(path, settings.AudioSampleRate);
        }
        if (settings.MaxMipMapLevel > 0)
        {
            ApplyMaxMipMapLevel(path, settings.MaxMipMapLevel);
        }
        if (settings.MaxTextureSize > 0)
        {
            ApplyMaxTextureSize(path, settings.MaxTextureSize);
        }
    }

    #region Apply Asset Settings

    private static void ApplyAudioCompressionFormat(string path, int format)
    {
        var assets = HelperFunctions.FindAssetsByType<AudioClip>(path);

        for (int i = 0; i < assets.Count; i++)
        {
            Preset preset = new Preset((AudioClip)assets[i].asset);

            var importer = AssetImporter.GetAtPath(assets[i].path);

            AudioImporter audioImporter = (AudioImporter)importer;
            AudioImporterSampleSettings audioImporterSampleSettings = audioImporter.defaultSampleSettings;

            audioImporterSampleSettings.compressionFormat = Keys.AudioCompressionFormats[format];

            audioImporter.defaultSampleSettings = audioImporterSampleSettings;

            bool result = preset.ApplyTo(importer);
            
            importer.SaveAndReimport();
        }
    }

    private static void ApplyAudioLoadType(string path, int loadType)
    {
        var assets = HelperFunctions.FindAssetsByType<AudioClip>(path);

        for (int i = 0; i < assets.Count; i++)
        {
            Preset preset = new Preset((AudioClip)assets[i].asset);

            var importer = AssetImporter.GetAtPath(assets[i].path);

            AudioImporter audioImporter = (AudioImporter)importer;
            AudioImporterSampleSettings audioImporterSampleSettings = audioImporter.defaultSampleSettings;

            audioImporterSampleSettings.loadType = Keys.AudioClipLoadTypes[loadType];

            audioImporter.defaultSampleSettings = audioImporterSampleSettings;

            bool result = preset.ApplyTo(importer);

            importer.SaveAndReimport();
        }
    }

    private static void ApplyAudioSampleRate(string path, int sampleRate)
    {
        var assets = HelperFunctions.FindAssetsByType<AudioClip>(path);

        for (int i = 0; i < assets.Count; i++)
        {
            Preset preset = new Preset((AudioClip)assets[i].asset);

            var importer = AssetImporter.GetAtPath(assets[i].path);

            AudioImporter audioImporter = (AudioImporter)importer;
            AudioImporterSampleSettings audioImporterSampleSettings = audioImporter.defaultSampleSettings;

            audioImporterSampleSettings.sampleRateOverride = Convert.ToUInt32(sampleRate);

            audioImporter.defaultSampleSettings = audioImporterSampleSettings;

            bool result = preset.ApplyTo(importer);

            importer.SaveAndReimport();
        }
    }

    private static void ApplyMaxMipMapLevel(string path, int maxMipMapLevel)
    {

    }

    private static void ApplyMaxTextureSize(string path, int maxTextureSize)
    {

    }

    #endregion

    /// <summary>
    /// Loops through all directories in the project and processes all configuration files
    /// </summary>
    /// <param name="path">The root path to search in</param>
    /// <param name="defaultSettings">The settings the current directory is inheriting from. Use this to programatically assign default settings</param>
    /// <returns>A directory structure with all app settings and directories represented</returns>
    private static DirectoryStructure GetImportSettings(string path, ImportSettings defaultSettings = null)
    {
        DirectoryStructure currentDirectory = new DirectoryStructure();
        currentDirectory.settings = HelperFunctions.JsonToClass<ImportSettings>(path, DirectoryInitializer.JsonConfigFileName);

        if (defaultSettings != null)
        {
            currentDirectory.settings.InheritSettings(defaultSettings);
        }

        currentDirectory.childSettings = new List<DirectoryStructure>();

        currentDirectory.path = path;

        List<string> childDirectoryPaths = DirectoryInitializer.FindDirectories(path, false);

        foreach(var currentChildDirectory in childDirectoryPaths)
        {
            DirectoryStructure childDirectoryStructure = new DirectoryStructure();

            childDirectoryStructure = GetImportSettings(currentChildDirectory, currentDirectory.settings);

            currentDirectory.childSettings.Add(childDirectoryStructure);
        }

        return currentDirectory;

        /*DirectoryStructure currentDirectory = new DirectoryStructure();

        currentDirectory.path = path;

        currentDirectory.settings = HelperFunctions.JsonToClass<ImportSettings>(path, DirectoryInitializer.JsonConfigFileName);

        currentDirectory.childSettings = new List<DirectoryStructure>();

        List<string> childDirectories = DirectoryInitializer.FindDirectories(path, false);

        foreach (var currentChild in childDirectories)
        {
            DirectoryStructure currentChildStructure = GetImportSettings(currentChild, currentDirectory.settings); // Reads the specified settings from JSON

            if (defaultSettings != null)
            {
                currentChildStructure.settings.InheritSettings(defaultSettings); // Applies default settings from parent directory JSON configuration file
            }

            currentDirectory.childSettings.Add(currentChildStructure);
        }

        return currentDirectory;*/
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;

namespace BitGames.CustomAssetImporter
{
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

        /// <summary>
        /// Recursively applies settings to all directories within the application
        /// </summary>
        /// <param name="structure">The directory structure to implement settings onto</param>
        private static void ApplySettingsRecursively(DirectoryStructure structure)
        {
            if (structure.settings.AndroidSettings.OverrideForAndroid == 1 && EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
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

        /// <summary>
        /// Applies android specific settings, also calls ApplyUniversalSettings() for settings shared with the Android platform
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="path"></param>
        private static void ApplyAndroidSettings(AndroidSettings settings, string path)
        {
            ApplyUniversalSettings((UniversalSettings)settings, path);
            // custom android settings that are not part of Universal Settings can be added here later
        }

        /// <summary>
        /// Applies Universal platform settings
        /// </summary>
        /// <param name="settings">The settings to apply</param>
        /// <param name="path">The path to apply the settings to</param>
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
            if (settings.TextureAnisoLevel > 0)
            {
                ApplyTextureAnisoLevel<Texture>(path, settings.TextureAnisoLevel);
                ApplyTextureAnisoLevel<Texture2D>(path, settings.TextureAnisoLevel);
            }
            if (settings.MaxTextureSize > 0)
            {
                ApplyMaxTextureSize<Texture>(path, settings.MaxTextureSize);
                ApplyMaxTextureSize<Texture2D>(path, settings.MaxTextureSize);
            }
            if (settings.TexturePixelsPerUnit > 0)
            {
                ApplyTexturePixelsPerUnit<Texture>(path, settings.TexturePixelsPerUnit);
                ApplyTexturePixelsPerUnit<Texture2D>(path, settings.TexturePixelsPerUnit);
            }
        }

        #region Apply Asset Settings

        /// <summary>
        /// Applies audio compression format settings to all audio files within the supplied directory. (non recursive)
        /// </summary>
        /// <param name="path">The path to search</param>
        /// <param name="format">The desired load format. Please see Keys.AudioCompressionFormats for more</param>
        private static void ApplyAudioCompressionFormat(string path, int format)
        {
            var assets = HelperFunctions.FindAssetsByType<AudioClip>(path);

            for (int i = 0; i < assets.Count; i++)
            {
                Preset preset = new Preset((AudioClip)assets[i].asset);

                var importer = AssetImporter.GetAtPath(assets[i].path);

                AudioImporter audioImporter = (AudioImporter)importer;
                AudioImporterSampleSettings audioImporterSampleSettings = audioImporter.defaultSampleSettings;

                try
                {
                    audioImporterSampleSettings.compressionFormat = Keys.AudioCompressionFormats[format];
                    audioImporter.defaultSampleSettings = audioImporterSampleSettings;

                    bool result = preset.ApplyTo(importer);

                    importer.SaveAndReimport();
                }
                catch
                {
                    Debug.LogError("Invalid Audio Compression Format");
                }
            }
        }

        /// <summary>
        /// Applies audio load type to all audio files within the supplied directory. (non recursive)
        /// </summary>
        /// <param name="path">The path to search</param>
        /// <param name="loadType">The desired load type for audio files. See Keys.AudioClipLoadTypes for more</param>
        private static void ApplyAudioLoadType(string path, int loadType)
        {
            var assets = HelperFunctions.FindAssetsByType<AudioClip>(path);

            for (int i = 0; i < assets.Count; i++)
            {

                try
                {
                    var importer = AssetImporter.GetAtPath(assets[i].path);

                    AudioImporter audioImporter = (AudioImporter)importer;
                 
                    AudioImporterSampleSettings audioImporterSampleSettings = audioImporter.defaultSampleSettings;
                    
                    audioImporterSampleSettings.loadType = Keys.AudioClipLoadTypes[loadType];

                    audioImporter.defaultSampleSettings = audioImporterSampleSettings;

                    importer.SaveAndReimport();
                }
                catch
                {
                    Debug.LogError("Invalid Audio Load Type");
                }
            }
        }

        /// <summary>
        /// Applies audio sample rate to all audio files within the supplied directory. (non recursive)
        /// </summary>
        /// <param name="path">The path to search</param>
        /// <param name="sampleRate">The sample rate to apply to audio files</param>
        private static void ApplyAudioSampleRate(string path, int sampleRate)
        {
            var assets = HelperFunctions.FindAssetsByType<AudioClip>(path);

            for (int i = 0; i < assets.Count; i++)
            {
                try
                {
                    var importer = AssetImporter.GetAtPath(assets[i].path);

                    AudioImporter audioImporter = (AudioImporter)importer;

                    AudioImporterSampleSettings audioImporterSampleSettings = audioImporter.defaultSampleSettings;

                    audioImporterSampleSettings.sampleRateSetting = AudioSampleRateSetting.OverrideSampleRate;

                    audioImporterSampleSettings.sampleRateOverride = Convert.ToUInt32(sampleRate);

                    audioImporter.defaultSampleSettings = audioImporterSampleSettings;

                    importer.SaveAndReimport();
                }
                catch
                {
                    Debug.LogError("Invalid audio sample rate");
                }
            }
        }

        /// <summary>
        /// Applies Aniso level to texture and texture2d objects within the supplied directory. (non recursive)
        /// </summary>
        /// <typeparam name="T">Textture or Texture2D</typeparam>
        /// <param name="path">The directory to apply the aniso level setting too</param>
        /// <param name="anisoLevel">The aniso level to be applied to textures</param>
        private static void ApplyTextureAnisoLevel<T>(string path, int anisoLevel) where T : UnityEngine.Object
        {
            var assets = HelperFunctions.FindAssetsByType<T>(path);

            for (int i = 0; i < assets.Count; i++)
            {
                try
                {
                    var importer = AssetImporter.GetAtPath(assets[i].path);

                    TextureImporter textureImporter = (TextureImporter)importer;

                    textureImporter.anisoLevel = anisoLevel;

                    importer = textureImporter;

                    importer.SaveAndReimport();
                }
                catch
                {
                    Debug.LogError("Invalid aniso level");
                }

            }
        }

        /// <summary>
        /// Applies pixels per inch for all textures within the supplied directory. (non recursive)
        /// </summary>
        /// <typeparam name="T">Texture or Texture2D</typeparam>
        /// <param name="path">The directory to apply the pixels per unit to</param>
        /// <param name="pixelsPerUnit">The pixels per unit to apply to textures</param>
        private static void ApplyTexturePixelsPerUnit<T>(string path, int pixelsPerUnit) where T : UnityEngine.Object
        {
            var assets = HelperFunctions.FindAssetsByType<T>(path);

            for (int i = 0; i < assets.Count; i++)
            {
                try
                {
                    var importer = AssetImporter.GetAtPath(assets[i].path);

                    TextureImporter textureImporter = (TextureImporter)importer;

                    textureImporter.spritePixelsPerUnit = pixelsPerUnit;

                    importer = textureImporter;

                    importer.SaveAndReimport();
                }
                catch
                {
                    Debug.LogError("Invalid pixels per Unit (texture)");
                }
            }
        }

        /// <summary>
        /// Applies the Max texture size attribute to all textures within the supplied directory. (non recursive)
        /// Leaves all other asset settings to their default state
        /// </summary>
        /// <typeparam name="T">Texture or Texture2D</typeparam>
        /// <param name="path">The directory to search</param>
        /// <param name="maxTextureSize">The desired max texture size</param>
        private static void ApplyMaxTextureSize<T>(string path, int maxTextureSize) where T : UnityEngine.Object
        {
            var assets = HelperFunctions.FindAssetsByType<T>(path);

            for (int i = 0; i < assets.Count; i++)
            {
                try
                {
                    var importer = AssetImporter.GetAtPath(assets[i].path);

                    TextureImporter textureImporter = (TextureImporter)importer;

                    textureImporter.maxTextureSize = maxTextureSize;

                    importer = textureImporter;

                    importer.SaveAndReimport();
                }
                catch
                {
                    Debug.LogError("Invalid Max Texture size");
                }
            }
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

            foreach (var currentChildDirectory in childDirectoryPaths)
            {
                DirectoryStructure childDirectoryStructure = new DirectoryStructure();

                childDirectoryStructure = GetImportSettings(currentChildDirectory, currentDirectory.settings);

                currentDirectory.childSettings.Add(childDirectoryStructure);
            }

            return currentDirectory;
        }
    }
}
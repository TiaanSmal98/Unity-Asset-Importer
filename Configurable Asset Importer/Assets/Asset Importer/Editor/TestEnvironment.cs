using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;

namespace BitGames.CustomAssetImporter
{
    public class TestEnvironment : EditorWindow
    {
        public string baseName { get; set; }

        public int myInt;

        public float myFloatSlider;

        public GameObject myGameObject;

        [MenuItem("Test/Configure/Asset Importer Tool")]
        public static void ShowWindow()
        {
            GetWindow(typeof(TestEnvironment), true, "My title");
        }

        private void OnGUI()
        {
            GUILayout.Label("Import Assets", EditorStyles.boldLabel);

            baseName = EditorGUILayout.TextField("Base Name", baseName);

            myInt = EditorGUILayout.IntField("My Int", 0);

            myFloatSlider = EditorGUILayout.Slider("My Slider", 0f, 0f, 100f);

            myGameObject = EditorGUILayout.ObjectField("My GO", myGameObject, typeof(GameObject), false) as GameObject;

            if (GUILayout.Button("My Button"))
            {
                ButtonClick();
            }
        }

        private void ButtonClick()
        {
            ApplySettings.ImportAndApplySettings();

            /*string path = "Assets\\Audio";

            var assetPaths = HelperFunctions.FindAssetsByType<AudioClip>(path, false);

            for (int i = 0; i < assetPaths.Count; i++)
            {
                Preset preset = new Preset((AudioClip)assetPaths[i].asset);

                //var proper = new PropertyModification();
                //proper.propertyPath = "m_Quality";
                //proper.value = "0.5";

                //preset.PropertyModifications.SetValue(proper, 19);

                var importer = AssetImporter.GetAtPath(assetPaths[i].path);

                AudioImporterSampleSettings a = new AudioImporterSampleSettings();
                a.sampleRateOverride = 8000;

                //importer.AddRemap(new AssetImporter.SourceAssetIdentifier(typeof(AudioClip), assetPaths[i].asset.name), a);

                importer.SaveAndReimport();

                AudioImporter audioImporter = (AudioImporter)importer;
                AudioImporterSampleSettings audioImporterSampleSettings = audioImporter.defaultSampleSettings;

                audioImporterSampleSettings.sampleRateOverride = 8000;

                audioImporterSampleSettings.loadType = AudioClipLoadType.DecompressOnLoad;
                audioImporter.defaultSampleSettings = audioImporterSampleSettings;

                //importer.defaultSampleSettings.sampleRateOverride = 8000;

                bool result = preset.ApplyTo(importer);

                Debug.Log(result);

                AssetDatabase.Refresh();
            }*/
        }
    }
}
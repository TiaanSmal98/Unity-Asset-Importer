using UnityEditor;
using UnityEngine;

namespace BitGames.CustomAssetImporter
{
    public class ApplyImportSettings : EditorWindow
    {
        [MenuItem("Asset Importer/Apply")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ApplyImportSettings), true, "Apply Import Settings");
        }

        private void OnGUI()
        {
            base.maxSize = new Vector2(500, 105f);
            base.minSize = base.maxSize;

            GUILayout.Label("Apply Import Rules", EditorStyles.boldLabel);
            GUILayout.Label("The JSON Import Settings are read, then applies those to the relevant asset files");
            GUILayout.Label("This process could take a while, depending on the size of your project");

            GUILayout.Space(25f);

            if (GUILayout.Button("Apply Import Settings"))
            {
                ApplySettings.ImportAndApplySettings();
                base.Close();
            }
        }
    }
}

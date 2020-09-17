using UnityEditor;
using UnityEngine;

namespace BitGames.CustomAssetImporter
{
    public class Help : EditorWindow
    {
        [MenuItem("Asset Importer/Help")]
        public static void ShowWindow()
        {
            GetWindow(typeof(Help), true, "Asset Impoter Help");
        }

        private void OnGUI()
        {
            var title = Color.magenta;
            var variable = Color.cyan;
            var textColor = Color.white;

            base.maxSize = new Vector2(625f, 743f);
            base.minSize = base.maxSize;

            GUILayout.Label("JSON Asset Importer Help", EditorStyles.boldLabel);
            GUILayout.Space(10f);

            GUI.color = title;
            GUILayout.Label("Universal Settings:", EditorStyles.boldLabel);
            GUILayout.Space(10f);
            GUI.color = textColor;

            GUILayout.Label("Applies to all platforms, where other platform settings (like Android) aren't overriding those settings");
            GUILayout.Label("All fields are initialized with -1, which would either result in inheriting settings from \nthe parent directory, or leaving the import settings to default.");


            GUI.color = title;
            GUILayout.Space(10f);
            GUILayout.Label("Android Settings:", EditorStyles.boldLabel);
            GUILayout.Space(10f);
            GUI.color = textColor;

            GUILayout.Label("Settings within Android settings are platform specific. \n" +
                "Where no Android settings are found, Universal settings will take effect\n\n" +
                "Override for Android field can be set to: \n" +
                "\t-1 : Inherit from parent directory \n" +
                "\t0 : Apply universal settings \n" +
                "\t1 : Apply Android Settings, where Android settings are not set to -1, otherwise use universal settings");


            GUI.color = title;
            GUILayout.Space(20f);
            GUILayout.Label("Settings:", EditorStyles.boldLabel);
            GUILayout.Space(10f);
            GUI.color = textColor;

            GUI.color = variable;
            GUILayout.Label("MaxTextureSize (int): ");
            GUI.color = textColor;
            GUILayout.Label("\t-1 : Apply from parent directory, if available, otherwise ignore \n" +
            "\tCombinations: 32, 64, 128, 256, 512, 1024, 2048, 4096, 8182");

            GUI.color = variable;
            GUILayout.Label("TextureAnisoLevel (int):");
            GUI.color = textColor;
            GUILayout.Label("\t-1 : Apply from parent directory, if available, otherwise ignore \n" +
            "\tCombinations: 1 through 16");

            GUI.color = variable;
            GUILayout.Label("TexturePixelsPerUnit (int): ");
            GUI.color = textColor;
            GUILayout.Label("\t-1 : Apply from parent directory, if available, otherwise ignore \n" +
            "\tCombinations: 1 through 2,147,483,647");

            GUI.color = variable;
            GUILayout.Label("AudioSampleRate (int): ");
            GUI.color = textColor;
            GUILayout.Label("\t-1 : Apply from parent directory, if available, otherwise ignore \n" +
            "\tCombinations (Hz): 8 000, 11 025, 22 050, 42 100, 48 000, 96 000, 192 000");

            GUI.color = variable;
            GUILayout.Label("AudioCompressionFormat (int): ");
            GUI.color = textColor;
            GUILayout.Label("\t-1 : Apply from parent directory, if available, otherwise ignore \n" +
            "\tValid Settings: \n " +
            "\t\t 1 : ADPCM \n" +
            "\t\t 2 : Vorbis \n" +
            "\t\t 3 : PCM");

            GUI.color = variable;
            GUILayout.Label("AudioLoadType (int): ");
            GUI.color = textColor;
            GUILayout.Label("\t-1 : Apply from parent directory, if available, otherwise ignore \n" +
            "\tValid Settings: \n " +
            "\t\t 1 : CompressedInMemory \n" +
            "\t\t 2 : DecompressOnLoad \n" +
            "\t\t 3 : Streaming");

            GUILayout.Space(10f);

            if (GUILayout.Button("Done"))
            {
                base.Close();
            }
        }
    }
}

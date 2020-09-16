using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class ResetConfigurations : EditorWindow
{
    [MenuItem("Asset Importer/Configure/Reset")]
    public static void ResetAllConfigurations()
    {
        GetWindow(typeof(ResetConfigurations), true, "Reset All Configurations");
    }

    private void OnGUI()
    {
        GUILayout.Label("Reset Assets", EditorStyles.boldLabel);
        GUILayout.Label("All platform Import Settings will be removed!", EditorStyles.label);
        GUILayout.Label("Directory ignore files are unaffected", EditorStyles.label);

        if (GUILayout.Button("Reset Configuration"))
        {
            DirectoryInitializer.InitializeDirectories(true);
        }
    }
}

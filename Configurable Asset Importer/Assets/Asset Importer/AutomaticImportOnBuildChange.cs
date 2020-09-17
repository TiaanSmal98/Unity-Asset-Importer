using UnityEngine;
using UnityEditor;
using UnityEditor.Build;

namespace BitGames.CustomAssetImporter
{
    public class AutomaticImportOnBuildChange : IActiveBuildTargetChanged
    {
        public int callbackOrder { get { return 0; } }

        public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
        {
            ApplySettings.ImportAndApplySettings();
        }
    }
}
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;

public class AutomaticImportOnBuildChange : IActiveBuildTargetChanged
{
    public int callbackOrder { get { return 0; } }

    public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
    {
        ApplySettings.ImportAndApplySettings();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class AndroidSettings : UniversalSettings
{
    public bool? OverrideForAndroid;

    /// <summary>
    /// Inherits settings from parent AndroidSettings class
    /// </summary>
    /// <param name="settings">The parents settings</param>
    public void InheritSettings(AndroidSettings settings)
    {
        if (this.OverrideForAndroid == null)
        {
            if (!settings.OverrideForAndroid == null)
            {
                this.OverrideForAndroid = settings.OverrideForAndroid;
            }
            else
            {
                this.OverrideForAndroid = false;
            }
        }

        base.InheritSettings(settings);
    }

    /// <summary>
    /// Initializes all variables within the class
    /// </summary>
    /// <returns>Returns itself</returns>
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public AndroidSettings Initialize()
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    {
        this.OverrideForAndroid = false;

        base.Initialize();

        return this;
    }
}

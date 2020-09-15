using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

[Serializable]
public class ImportSettings
{
    public UniversalSettings UniversalSettings;
    public AndroidSettings AndroidSettings;

    /// <summary>
    /// Inherits settings from parent Import Settings class
    /// </summary>
    /// <param name="settings">The parents settings</param>
    public ImportSettings InheritSettings(ImportSettings parentSettings)
    {
        this.UniversalSettings.InheritSettings(parentSettings.UniversalSettings);
        this.AndroidSettings.InheritSettings(parentSettings.AndroidSettings);

        return this;
    }

    /// <summary>
    /// Initializes all variables within the class
    /// </summary>
    /// <returns>Returns itself</returns>
    public ImportSettings Initialize()
    {
        this.UniversalSettings = new UniversalSettings().Initialize();

        this.AndroidSettings = new AndroidSettings().Initialize();

        return this;
    }

}

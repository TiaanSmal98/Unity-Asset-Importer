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

    public ImportSettings InheritSettings(ImportSettings parentSettings)
    {
        this.UniversalSettings.InheritSettings(parentSettings.UniversalSettings);
        this.AndroidSettings.InheritSettings(parentSettings.AndroidSettings);

        return this;
    }

    public ImportSettings Initialize()
    {
        this.UniversalSettings = new UniversalSettings().Initialize();

        this.AndroidSettings = new AndroidSettings().Initialize();

        return this;
    }

}

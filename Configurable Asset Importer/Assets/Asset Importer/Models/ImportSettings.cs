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
}

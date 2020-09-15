using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class AndroidSettings : UniversalSettings
{
    public bool? OverrideForAndroid;

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

    public AndroidSettings Initialize()
    {
        this.OverrideForAndroid = false;

        base.Initialize();

        return this;
    }


}

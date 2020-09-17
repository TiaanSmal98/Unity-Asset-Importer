using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitGames.CustomAssetImporter
{
    [Serializable]
    public class AndroidSettings : UniversalSettings
    {
        public int OverrideForAndroid;

        /// <summary>
        /// Inherits settings from parent AndroidSettings class
        /// </summary>
        /// <param name="settings">The parents settings</param>
        public void InheritSettings(AndroidSettings settings)
        {
            if (this.OverrideForAndroid < 0)
            {
                this.OverrideForAndroid = settings.OverrideForAndroid;
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
            this.OverrideForAndroid = -1;

            base.Initialize();

            return this;
        }
    }
}
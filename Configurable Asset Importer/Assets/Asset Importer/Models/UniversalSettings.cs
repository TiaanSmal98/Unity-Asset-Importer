using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class UniversalSettings
{
    // Textures
    public int MaxTextureSize;
    public int MaxMipMapLevel;

    //Audio Files
    public int AudioSampleRate;
    public int AudioCompressionFormat;
    public int AudioLoadType;

    /// <summary>
    /// Inherits settings from parent Universal platform class
    /// </summary>
    /// <param name="settings">The parents settings</param>
    public void InheritSettings(UniversalSettings settings)
    {
        if (this.MaxTextureSize < 0)
            this.MaxTextureSize = settings.MaxTextureSize;

        if (this.MaxMipMapLevel < 0)
            this.MaxMipMapLevel = settings.MaxMipMapLevel;

        if (this.AudioSampleRate < 0)
            this.AudioSampleRate = settings.AudioSampleRate;

        if (this.AudioCompressionFormat < 0)
            this.AudioCompressionFormat = settings.AudioCompressionFormat;

        if (this.AudioLoadType < 0)
            this.AudioLoadType = settings.AudioLoadType;
    }

    /// <summary>
    /// Initializes all variables within the class
    /// </summary>
    /// <returns>Returns itself</returns>
    public UniversalSettings Initialize()
    {
        this.MaxTextureSize = -1;

        this.MaxMipMapLevel = -1;

        this.AudioSampleRate = -1;

        this.AudioCompressionFormat = -1;

        this.AudioLoadType = -1;

        return this;
    }

}

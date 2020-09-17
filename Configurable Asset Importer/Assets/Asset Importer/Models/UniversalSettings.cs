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
    public int TextureAnisoLevel;
    public int TexturePixelsPerUnit;

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
        if (this.TextureAnisoLevel < 0)
            this.TextureAnisoLevel = settings.TextureAnisoLevel;

        if (this.TexturePixelsPerUnit < 0)
            this.TexturePixelsPerUnit = settings.TexturePixelsPerUnit;

        if (this.MaxTextureSize < 0)
            this.MaxTextureSize = settings.MaxTextureSize;

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

        this.AudioSampleRate = -1;

        this.AudioCompressionFormat = -1;

        this.AudioLoadType = -1;

        this.TextureAnisoLevel = -1;

        this.TexturePixelsPerUnit = -1;

        return this;
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class UniversalSettings
{
    // Textures
    public int? MaxTextureSize;
    public int? MaxMipMapLevel;

    //Audio Files
    public int? AudioSampleRate;
    public int? AudioCompressionFormat;
    public int? AudioLoadType;
}

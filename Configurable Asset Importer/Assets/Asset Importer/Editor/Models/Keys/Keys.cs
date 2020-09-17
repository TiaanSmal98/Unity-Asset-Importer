using System.Collections.Generic;
using UnityEngine;

namespace BitGames.CustomAssetImporter
{
    public static class Keys
    {
        public static Dictionary<int, AudioCompressionFormat> AudioCompressionFormats = new Dictionary<int, AudioCompressionFormat> {
        {1, AudioCompressionFormat.ADPCM },
        {2, AudioCompressionFormat.Vorbis },
        {3, AudioCompressionFormat.PCM }
    };

        public static Dictionary<int, AudioClipLoadType> AudioClipLoadTypes = new Dictionary<int, AudioClipLoadType>
    {
        {1, AudioClipLoadType.CompressedInMemory },
        {2, AudioClipLoadType.DecompressOnLoad },
        {3, AudioClipLoadType.Streaming }
    };
    }
}
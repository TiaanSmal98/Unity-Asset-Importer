using System.Collections.Generic;
using UnityEngine;

public static class Keys
{
    public static Dictionary<int, AudioCompressionFormat> AudioCompressionFormats = new Dictionary<int, AudioCompressionFormat> {
        {1, AudioCompressionFormat.AAC },
        {2, AudioCompressionFormat.ADPCM },
        {3, AudioCompressionFormat.ATRAC9 },
        {4, AudioCompressionFormat.GCADPCM },
        {5, AudioCompressionFormat.HEVAG },
        {6, AudioCompressionFormat.MP3 },
        {7, AudioCompressionFormat.PCM },
        {8, AudioCompressionFormat.VAG },
        {9, AudioCompressionFormat.Vorbis },
        {10,AudioCompressionFormat.XMA }
    };

    public static Dictionary<int, AudioClipLoadType> AudioClipLoadTypes = new Dictionary<int, AudioClipLoadType>
    {
        {1, AudioClipLoadType.CompressedInMemory },
        {2, AudioClipLoadType.DecompressOnLoad },
        {3, AudioClipLoadType.Streaming }
    };
}
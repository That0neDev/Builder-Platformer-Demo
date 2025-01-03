using UnityEngine;
using System.Collections.Generic;

namespace Behaviours.Tiles{
    [System.Serializable]
    public class TileSoundData{
        public TileMaterial tileMat;
        public List<AudioClip> audioClips;
        public AudioClip GetClip(){
            return audioClips[Random.Range(0, audioClips.Count)];
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Behaviours.Player
{
    public class StepSoundHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource Source;
        [SerializeField] List<AudioClip> Clips;

        public void PlayStep(){
            Source.clip = Clips[Random.Range(0,Clips.Count)];
            Source.Play(); 
        }
    } 
}

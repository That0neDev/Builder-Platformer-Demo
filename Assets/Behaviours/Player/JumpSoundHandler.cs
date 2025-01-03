using UnityEngine;

namespace Behaviours.Player
{
    public class JumpSoundHandler : MonoBehaviour
    {
        [SerializeField] AudioSource Source;
        [SerializeField] AudioClip jumpClip;
        public void Play(){
            Source.clip = jumpClip;
            Source.Play(); 
        }
    }
}

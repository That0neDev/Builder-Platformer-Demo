using System;
using UnityEngine;


namespace Behaviours.Player{
    public class PlayerAnimator : MonoBehaviour
    {
        public Animator Player;
        private string state;
        public Action<string> OnAnimationEvent;
        [SerializeField] PlayerData playerData;

        private void Update()
        {
            if (playerData.movementValues.walkInput == 0)
                return;

            bool flip = playerData.movementValues.walkInput < 0;
            float y = flip ? -180 : 0;
            transform.rotation = Quaternion.Euler(0, y, 0);
        }
        private void Play(string Value)
        {
            if (state == Value)
                return;

            state = Value;
            Player.Play(Value);
        }

        private void Start()
        {
            Play("Idle");
            OnAnimationEvent += Play;
        }
    }
}

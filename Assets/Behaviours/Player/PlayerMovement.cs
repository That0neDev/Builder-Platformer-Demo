using System;
using UnityEngine;

namespace Behaviours.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private PlayerAnimator playerAnimator;
        [SerializeField] private Rigidbody2D body2D;
        [SerializeField] JumpSoundHandler jumpSFX;
        
        private void Awake(){
            playerData = PlayerData.GetPlayerData();
        }

        private void FixedUpdate()
        {
            XMovement();
            YMovement();
        }

        private void XMovement()
        {
            var m = playerData.movementValues;
            if(ShouldSlowDown()){
                body2D.linearVelocityX *= playerData.moveData.Drag;
                
                if(Math.Abs(body2D.linearVelocityX) < playerData.moveData.MinX)
                    body2D.linearVelocityX = 0;
            } else {
                
                body2D.linearVelocityX += m.walkInput * playerData.moveData.Acceleration * Time.fixedDeltaTime;
                body2D.linearVelocityX = Mathf.Clamp(body2D.linearVelocityX,-playerData.moveData.MaxX,playerData.moveData.MaxX);
            }


            if (!m.grounded)
                playerAnimator.OnAnimationEvent.Invoke("Jump");
            if (body2D.linearVelocityX != 0 && m.grounded)
                playerAnimator.OnAnimationEvent.Invoke("Walk");
            if (body2D.linearVelocityX == 0 && m.grounded)
                playerAnimator.OnAnimationEvent.Invoke("Idle");

            return;

            bool ShouldSlowDown(){
                int speed = (int)body2D.linearVelocityX;
                var direction   = m.walkInput;
                
                if (direction == 0)
                    return true;

                if(speed == 0 && direction != 0)
                    return false;
                
                return Math.Sign(speed) != Math.Sign(direction);
            }
        }
        
        private void YMovement(){
            var m = playerData.movementValues;
            if(m.jumpInput != 0 && m.grounded){
                body2D.linearVelocityY = playerData.moveData.MaxY;
                playerAnimator.OnAnimationEvent.Invoke("Jump");
                jumpSFX.Play();
            }
            
            body2D.linearVelocityY += -playerData.moveData.Gravity * Time.fixedDeltaTime;
            body2D.linearVelocityY = Mathf.Clamp(body2D.linearVelocityY,playerData.moveData.MinY,playerData.moveData.MaxY);
        }
    }
}
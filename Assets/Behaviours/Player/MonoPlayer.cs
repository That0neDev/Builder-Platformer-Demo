using Behaviours.Tiles;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Behaviours.Player
{
    public class MonoPlayer : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private StepSoundHandler soundHandler;
        [SerializeField] PlayerData playerData;

        private bool Grounded()
        {
            var left  = (Vector2)transform.position + new Vector2(-.35f, -0.85f);
            var right = (Vector2)transform.position + new Vector2(0.35f, -0.85f);

            RaycastHit2D hitLeft = Physics2D.Raycast(left, Vector2.down, 0.2f, groundLayer);
            RaycastHit2D hitRight = Physics2D.Raycast(right, Vector2.down, 0.2f, groundLayer);
            bool retVal = hitLeft.collider != null || hitRight.collider != null;
            return retVal;
        }
        private void Update()
        {
            playerData.movementValues.grounded = Grounded();
        }
    }
}


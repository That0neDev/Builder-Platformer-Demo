using UnityEngine;

namespace Behaviours.PlayerObjects.Instances
{
    [CreateAssetMenu(fileName = "PlayerObject", menuName = "Scriptable Objects/New Block")]
    public class Block : PlayerObject{
        [Header("Block related data")]
        public float Durability;
        public LayerMask UnallowedLayers;

        public override bool CalculateCollision(Vector2 colliderSize,Vector2 snapPos)
        {  
            Collider2D HitBox = Physics2D.OverlapBox(snapPos,colliderSize * 0.95f,0,UnallowedLayers);
            return HitBox == null;
        }
    }
}
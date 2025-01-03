using System;
using UnityEngine;

namespace Behaviours.PlayerObjects
{
    public class PlayerObject : ScriptableObject{
        [Header("Object data")]
        public GameObject Prefab;
        public bool isStatic;
        public virtual bool CalculateCollision(Vector2 colliderSize,Vector2 snapPos){
            return true;
        }
    }
}
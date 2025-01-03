using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Behaviours.Player
{
    [System.Serializable]
    public class PlayerData : MonoBehaviour{
        public LayerMask Interactable;
        public Tilemap ActiveTilemap;

        public MovementData movementValues;
        public GemData gemData;
        public MoveData moveData;

        public static PlayerData GetPlayerData(){
            return GameObject.FindFirstObjectByType<PlayerData>();
        }
    }

    [Serializable]
    public class MovementData
    {
        public bool grounded;
        public float walkInput;
        public float jumpInput;
        public int activeOrbIndex;
    }

    [Serializable]
    public class GemData{
        public int Green;
        public int Red;
        public int Blue;
    }

    [Serializable]
    public class MoveData
    {
        public float Drag;
        public float Acceleration;
        public int MinX;
        public int MaxX;
        public float Gravity;
        public float MinY;
        public float MaxY;
    }
}
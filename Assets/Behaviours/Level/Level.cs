using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Behaviours.PlayerObjects;

namespace Behaviours.Levels
{
    public class Level : MonoBehaviour
    {
        private Transform player;
        public List<PlayerObjectData> Objects;
        [SerializeField] CinemachineCamera cineCam;
        [SerializeField] Transform startObj;
        [SerializeField] Canvas levelCanvas;

        public void LoadLevel(GameGlobal instance){
            GameGlobal g = instance;
            player = g.Player.transform;
            g.ActiveLevel = this;
            InitLevel();
        }

        private void InitLevel(){
            player.gameObject.SetActive(true);
            player.position = startObj.position;
            levelCanvas.worldCamera = Camera.main;
            cineCam.enabled = true;
            cineCam.Follow = player;
        }
    }
}

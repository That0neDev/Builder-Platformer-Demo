using UnityEngine;

namespace Behaviours.Levels.LevelObjects
{
    public class Spike : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider2D){
            GameGlobal g = GameGlobal.GetGlobal();
            if(collider2D.gameObject != g.Player)
                return;
            
            g.Player.SetActive(false);
            g.LoadSameLevel();
        }
    }
}


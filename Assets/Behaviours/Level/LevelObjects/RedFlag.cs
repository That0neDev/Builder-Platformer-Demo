using Behaviours.Interfaces;
using UnityEngine;

namespace Behaviours.Levels.LevelObjects
{
    public class RedFlag : MonoBehaviour, IInteractable
    {
        private void LoadNewLevel(){
            GameGlobal g = GameGlobal.GetGlobal();
            g.LoadNextLevel();
        }

        public void Interact()
        {
            LoadNewLevel();
        }
    }
}


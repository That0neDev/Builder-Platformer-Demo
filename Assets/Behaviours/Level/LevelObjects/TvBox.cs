using Behaviours.Interfaces;
using Behaviours.UI;
using UnityEngine;

namespace Behaviours.Levels.LevelObjects
{
    public class TvBox : MonoBehaviour , IInteractable{

        [SerializeField] UIElement targetUI;

        public void Interact()
        {
            FindFirstObjectByType<GameGlobal>().GameUI.OpenUI(targetUI);
        }
    }
}
using Behaviours.Interfaces;
using Behaviours.PlayerObjects;
using Behaviours.UI;
using TMPro;
using UnityEngine;

namespace Behaviours.UI.Building
{
    public class BuildElement : MonoBehaviour, ISelectable{
        public BuildPanel panel;
        public PlayerObject objData;
        [SerializeField] TextMeshProUGUI amountHolder;
        public void Select()
        {
            panel.OnSelectedElement(transform.GetSiblingIndex());
        }

        public void OpenBuildMode(){

        }

        public void CloseBuildMode(){

        }
    }
}

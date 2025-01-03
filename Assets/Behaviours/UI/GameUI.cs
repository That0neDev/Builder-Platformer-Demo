using System.Collections.Generic;
using System.Linq;
using Behaviours.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Behaviours.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] PlayerInputHandler playerInput;
        [SerializeField] HashSet<UIElement> activeElements = new();
        [SerializeField] UIElement lastTarget = null;

        public Vector2 inputDirection;

        [Header("Serialized UI Elements")]
        public UIElement buildUI;
        public void SetActive(UIElement target){
            if(target.isOpen)
                return;
            
            lastTarget = target;
        }
        public void OpenUI(UIElement target){
            if(target.grabAttention)
                lastTarget = target;
            
            activeElements.Add(target.Open());
            HashSet<UIElement> dependencies = new();
            target.GetDependencies(ref dependencies);

            foreach (var element in dependencies)
                activeElements.Add(element.Open());
        }
        public void CloseUI(UIElement target){
            HashSet<UIElement> dependencies = new();
            target.GetDependencies(ref dependencies);
            foreach (var element in dependencies)
                activeElements.Remove(element.Close());
            
            target.Close();
            if(target.returnElement != null)
                OpenUI(target.returnElement);
        }
        public void ResetUI(){
            foreach (var element in activeElements){
                element.Close();
            }

            activeElements.Clear();
            lastTarget = null;
        }
        private void ReturnToGame(){
            ResetUI();
            playerInput.SetMap("GamePlay");
        }
        private void Return(){
            activeElements.Remove(lastTarget.Close());
            if(lastTarget.returnElement == null){
                ReturnToGame();
                return;
            }
            lastTarget = lastTarget.returnElement.Restart();
        }

        public void OnEscape(InputAction.CallbackContext ctx){
            if(ctx.phase != InputActionPhase.Started)
                return;
            
            print($"Closing {lastTarget}!");
            if(lastTarget != null)
                Return();
            else ReturnToGame();
        }
        public void OnTab(InputAction.CallbackContext ctx){
            if(ctx.phase != InputActionPhase.Started)
                return;
            
            if(lastTarget == null){
                playerInput.SetMap("UI");
                OpenUI(buildUI);
            }
        }
        public void OnSelect(InputAction.CallbackContext ctx){

            if(ctx.phase != InputActionPhase.Started)
                return;
            

            GameObject selected = EventSystem.current.currentSelectedGameObject;
            if(selected == null)
                return;

            selected.TryGetComponent(out ISelectable selectable);
            selectable?.Select();
        }
        public void OnNavigate(InputAction.CallbackContext ctx){
            inputDirection = ctx.ReadValue<Vector2>();
        }
    
        public void Update(){
            //print(EventSystem.current.currentSelectedGameObject);
        }
    }
}


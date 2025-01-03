using UnityEngine;
using UnityEngine.InputSystem;
using Behaviours.Player;
using Behaviours.Interfaces;

public class PlayerInputHandler : MonoBehaviour {

    [SerializeField] PlayerData playerData;
    [SerializeField] PlayerInput playerInput;
    public bool isActive = false;

    private void Awake(){
        playerInput.actions.Enable();
        SetMap("GamePlay");
        isActive = true;
    }

    public void Enable(){
        playerInput.actions.Enable();
    }
    public void Disable(){
        playerInput.actions.Disable();
    }
    public void SetMap(string value){
        playerInput.SwitchCurrentActionMap(value);
    }
    

    public void OnWalkInput(InputAction.CallbackContext context)
    {
            playerData.movementValues.walkInput = context.ReadValue<float>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
            playerData.movementValues.jumpInput = context.ReadValue<float>();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.action.phase != InputActionPhase.Started)
            return;

        Collider2D hit = Physics2D.OverlapCircle(transform.position, 2, playerData.Interactable);

        if (hit != null)
            hit.gameObject.GetComponent<IInteractable>().Interact();
    }

    public void OnResetInput(InputAction.CallbackContext _){
        GameGlobal.GetGlobal().LoadSameLevel();
    }
}
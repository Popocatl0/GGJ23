using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Handle the inputs and trigger the actions for the PlayerController
/// </summary>
public class InputManager : MonoBehaviour{
    public PlayerController controller{ get; private set;}

    #region INPUT_VALUES
    public Vector2 movement {get; private set;}
    public bool isFire {get; private set;}
    #endregion

    #region  INPUT_ACTIONS
    public InputActionAsset inputAsset;
    private InputActionMap playerControls;
    private InputAction inputMovement;
    private InputAction inputFire;
    #endregion

    /// <summary>
    /// Assign a ship
    /// </summary>
    /// <param name="_controller"></param>
    public void SetController(PlayerController _controller){
        controller = _controller;
        SetInputs();
    }
    
    /// <summary>
    /// Search a input map by ship/controller ID,
    /// and set the Input Values with its respective Input Action
    /// </summary>
    void SetInputs(){
        playerControls = inputAsset.FindActionMap(controller.ID);
        inputMovement = playerControls.FindAction("Turn");
        inputFire = playerControls.FindAction("Fire");

        inputMovement.performed += context => OnMove(context.ReadValue<Vector2>());
        inputFire.performed += context => OnFire(context.performed);


        inputMovement.canceled += context => OnMove(context.ReadValue<Vector2>());
        inputFire.canceled += context => OnFire(context.performed);

        playerControls.Enable();
    }

    public void OnFire(bool val){
        isFire = val;
        controller.UpdateInput();
    }
    public void OnMove(Vector2 dir){
        movement = dir;
        controller.UpdateInput();
    }
}

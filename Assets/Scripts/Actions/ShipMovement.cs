    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handle the ship's inertia and maximum speed
/// </summary>
public class ShipMovement : ShipAction{
    float turnDir;
    Vector2 direction;
    public override void Init(ShipController _contr)
    {
        base.Init(_contr);
        turnDir = 0;
        direction = Vector2.zero;
    }
    /// <summary>
    /// Called when a input is received
    /// </summary>
    public override void BeginAction()
    {
        BeginTurn();
    }
    /// <summary>
    /// Called every cycle in FixedUpdate
    /// </summary>
    public override void ProcessFixedAction(){
        Impulse();
    }

    /// <summary>
    /// Stop all ship's movement 
    /// </summary>
    public override void StopAction(){
        if(controller.currentSpeed > 0){
            controller.currentSpeed = 0;
            controller.Rigbody.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Handle the intertia
    /// </summary>
    void Impulse(){
        if (controller.currentSpeed < controller.currentMaxSpeed){
            controller.currentSpeed = controller.Data.smoothingFactor == 0? controller.currentMaxSpeed : Mathf.Lerp(controller.currentSpeed, controller.currentMaxSpeed, 1/controller.Data.smoothingFactor);
        }
        controller.Rigbody.velocity = controller.currentDir.normalized * controller.currentSpeed;
        if (turnDir != 0)
        {
            controller.currentDir = Quaternion.Euler(0, 0, -turnDir * controller.Data.turnSpeed * Time.deltaTime) * controller.currentDir;
            this.transform.up = -controller.currentDir;
        }
    }

    /// <summary>
    /// Check the input value and modify the ship's speed
    /// 0 => Max Speed
    /// 1 o -1 => Reduce Speed 
    /// </summary>
    public void BeginTurn()
    {
        direction = controller.Input.movement;
        controller.currentMaxSpeed = controller.Input.movement.magnitude > 0 ? (controller.Data.friction * controller.Data.maxSpeed) : controller.Data.maxSpeed;
        controller.currentSpeed = controller.currentSpeed > controller.currentMaxSpeed ? controller.currentMaxSpeed : controller.currentSpeed * controller.Data.friction;
    }
}

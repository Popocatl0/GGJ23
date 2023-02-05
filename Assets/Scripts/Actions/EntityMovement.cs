using System.Security.Cryptography;
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

/// <summary>
/// Class that handle the ship's inertia and maximum speed
/// </summary>
public class EntityMovement : EntityAction{
    Vector2 direction;
    public MMFeedbacks walkFeedback;
    public override void Init(PlayerController _contr)
    {
        base.Init(_contr);
        direction = Vector2.zero;
    }
    /// <summary>
    /// Called when a input is received
    /// </summary>
    public override void BeginAction(){
        direction = controller.Input.movement;
        if(direction != Vector2.zero) controller.currentDir= direction.normalized;
    }
    /// <summary>
    /// Called every cycle in FixedUpdate
    /// </summary>
    public override void ProcessFixedAction(){
        Move();
    }

    /// <summary>
    /// Stop all enity's movement 
    /// </summary>
    public override void StopAction(){
        controller.Rigbody.velocity = Vector2.zero;
    }

    /// <summary>
    /// Handle the intertia
    /// </summary>
    void Move(){
        direction = controller.Input.movement;
        controller.Rigbody.velocity = direction.normalized * controller.Data.maxSpeed;
        if(direction != Vector2.zero){
            controller.currentDir= direction.normalized;
            walkFeedback.PlayFeedbacks();
        } 
        controller.Render.flipX = direction.x > 0;
    }

    /*public void BeginTurn(){
        if (controller.currentSpeed < controller.currentMaxSpeed){
            controller.currentSpeed = controller.Data.smoothingFactor == 0? controller.currentMaxSpeed : Mathf.Lerp(controller.currentSpeed, controller.currentMaxSpeed, 1/controller.Data.smoothingFactor);
        }
        controller.currentMaxSpeed = controller.Input.movement.magnitude > 0 ? (controller.Data.friction * controller.Data.maxSpeed) : controller.Data.maxSpeed;
        controller.currentSpeed = controller.currentSpeed > controller.currentMaxSpeed ? controller.currentMaxSpeed : controller.currentSpeed * controller.Data.friction;
    }*/
}

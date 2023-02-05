using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Target of the Player
/// </summary>
public class PlayerTarget : MonoBehaviour{

    PlayerController  controller;
    public CarryItem target {get; private set;}
    /// <summary>
    /// Assign a ship 
    /// </summary>
    /// <param name="_controller"></param>
    public void SetController(PlayerController _controller){
        controller = _controller;
    }

    void Update(){
        if (controller.currentDir == Vector2.zero)
            this.transform.position = Vector2.right * controller.Data.targetDistance;
        else
            this.transform.position = (Vector2)controller.transform.position + (controller.currentDir * controller.Data.targetDistance);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        target = other.GetComponent<CarryItem>();
    }

    void OnTriggerExit2D(Collider2D other){
        if(target == other.GetComponent<CarryItem>())
            target = null;
    } 
}

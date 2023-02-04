using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Target of the Player
/// </summary>
public class PlayerTarget : MonoBehaviour{

    PlayerController  controller;
    Vector2 discretePosition;
    public GameObject target {get; private set;}
    /// <summary>
    /// Assign a ship 
    /// </summary>
    /// <param name="_controller"></param>
    public void SetController(PlayerController _controller){
        controller = _controller;
    }

    void Update(){
        if(controller.currentDir == Vector2.zero)
            discretePosition = Vector2.right * controller.Data.targetDistance;
        else
            discretePosition = (Vector2)controller.transform.position + (controller.currentDir * controller.Data.targetDistance);

        discretePosition.x = Mathf.Floor(discretePosition.x) + 0.5f;
        discretePosition.y = Mathf.Floor(discretePosition.y) + 0.5f;

        this.transform.position = discretePosition;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        target = other.gameObject;
    }

    void OnTriggerExit2D(Collider2D other){
        if(target == other.gameObject)
            target = null;
    } 
}

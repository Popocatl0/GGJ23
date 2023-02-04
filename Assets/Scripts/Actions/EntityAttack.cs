using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Class for handle the Attack or Shoot 
/// </summary>
public class EntityAttack : EntityAction {
    [SerializeField] BulletItem bulletItem;//carry item
    public Transform carryPos;

     public override void Init(PlayerController _contr){
        base.Init(_contr);
    }
    
    /// <summary>
    /// Only shoot if the input hold pressed
    /// </summary>
    public override void BeginAction(){
        if(controller.Input.isFire)
            Shoot();
    }
    /// <summary>
    /// Check the timer and shoot one bullet from the pool
    /// </summary>
    void Shoot(){
        //if empty carry target
        //if bomb throw on direction
        //if barrier set on target
    }
}

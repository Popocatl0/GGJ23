using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Class for handle the Attack or Shoot 
/// </summary>
public class EntityAttack : EntityAction {
    //[SerializeField] BulletItem bulletItem;//carry item
    GameObject carryObj;

     public override void Init(PlayerController _contr){
        base.Init(_contr);
        carryObj = null;
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
        if(carryObj == null && controller.Target.target != null){
            Debug.Log("Carry and OBJ");
            carryObj = controller.Target.target;//get Obj
        }
        //if carry = bomb  throw on direction
        //if barrier = set on target
    }
}

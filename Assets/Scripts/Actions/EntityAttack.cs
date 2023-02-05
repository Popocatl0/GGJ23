using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Class for handle the Attack or Shoot 
/// </summary>
public class EntityAttack : EntityAction {
    //[SerializeField] BulletItem bulletItem;//carry item
    CarryItem carryObj;
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
    public override void StopAction(){
        base.StopAction();
        if (carryObj.GetType() == typeof(BombItem)) {
            ((BombItem)carryObj).DisableBomb();
        }
        else if (carryObj.GetType() == typeof(BarrierItem)){
            carryObj.OnSet(controller);
        }
        carryObj = null;
    }
    /// <summary>
    /// Check the timer and shoot one bullet from the pool
    /// </summary>
    void Shoot(){
        if (carryObj == null && controller.Target.target != null) {
            carryObj = controller.Target.target.OnCarry();
            if (carryObj != null) carryObj.Init(controller.CarryPos);
        }
        else if (carryObj != null && controller.Target.target == null){
            carryObj.OnSet(controller);
            carryObj = null;
        }
        else if(carryObj != null && controller.Target.target != null){
            carryObj.OnCharge(controller.Target.target);
        }
    }
}

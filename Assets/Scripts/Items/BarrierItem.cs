using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierItem : CarryItem{
    Rigidbody2D rigbody;
    Collider2D colliderBomb;
    void Awake()
    {
        rigbody = GetComponent<Rigidbody2D>();
        colliderBomb = GetComponent<Collider2D>();
    }

    public override CarryItem OnCarry(){
        return this;
    }

    public override void OnSet(PlayerController controller){
        this.transform.parent = null;
        rigbody.bodyType = RigidbodyType2D.Static;
        this.transform.position = controller.Target.transform.position;
        colliderBomb.enabled = true;
    }

    public override void Init(Transform parent){
        this.transform.parent = parent;
        rigbody.bodyType = RigidbodyType2D.Kinematic;
        this.transform.position = parent.position;
        colliderBomb.enabled = false;
    }

    public override void OnCharge(CarryItem item)
    {
    }
}

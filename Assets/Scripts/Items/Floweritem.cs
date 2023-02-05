using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floweritem : CarryItem{

    BombPool pool;
    public float TimeRespawn;
    float timer = 0;
    private void Awake(){
        pool = FindObjectOfType<BombPool>();
        pool.Init();
    }

    private void Update(){
        if (timer > 0) timer -= Time.deltaTime;
        //else feedback
    }
    public override CarryItem OnCarry(){
        if (timer <= 0){
            timer = TimeRespawn;
            //feedback;
            return pool.GetBullet();
        }
        else return null;
    }

    public override void OnSet(PlayerController controller){

    }

    public override void Init(Transform parent){
    }

    public override void OnCharge(CarryItem item)
    {
    }
}

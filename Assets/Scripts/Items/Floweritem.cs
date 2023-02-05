using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Floweritem : CarryItem{

    BombPool pool;
    TextMeshPro counter;
    public float TimeRespawn;
    float timer = 0;
    private void Awake(){
        pool = FindObjectOfType<BombPool>();
        pool.Init();
        counter = GetComponentInChildren<TextMeshPro>();
        counter.gameObject.SetActive(false);
    }

    private void Update(){
        if (timer > 0){
            timer -= Time.deltaTime;
            counter.text = Mathf.Ceil(timer).ToString("00");
        } 
        else{
            counter.gameObject.SetActive(false);
        }
        
            
        //else feedback
    }
    public override CarryItem OnCarry(){
        if (timer <= 0){
            timer = TimeRespawn;
            counter.gameObject.SetActive(true);
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

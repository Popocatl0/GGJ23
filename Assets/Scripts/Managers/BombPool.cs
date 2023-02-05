using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class BombPool : MonoBehaviour {
    [SerializeField] BombItem bulletItem;
    public int maxPoolSize;
    Stack<BombItem> bulletPool;

    public void Init(){
        bulletPool = new Stack<BombItem>(maxPoolSize);
    }

    /// <summary>
    /// Get one bullet from the pool or create new one
    /// </summary>
    public BombItem GetBullet(){
        BombItem bullet;
        if(!bulletPool.TryPop(out bullet)){
            bullet = Instantiate(bulletItem);
            bullet.onDestroyCollision += OnBombDestroy;
        }
        return bullet;
        //bullet.Set(controller.currentDir, shootPos.position);
    }

    /// <summary>
    /// Destroy the bullet and return it to the pool
    /// Also call the respective Damage and Score methods
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="target"></param>
    void OnBombDestroy(BombItem bullet, PlayerController target){
        bulletPool.Push(bullet);
    }
    
        
}

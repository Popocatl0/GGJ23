using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class BombPool : MonoBehaviour {
    [SerializeField] BulletItem bulletItem;
    public int maxPoolSize;
    Stack<BulletItem> bulletPool;

    public void Init(){
        bulletPool = new Stack<BulletItem>(maxPoolSize);
    }

    /// <summary>
    /// Get one bullet from the pool or create new one
    /// </summary>
    BulletItem GetBullet(){
        BulletItem bullet;
        if(!bulletPool.TryPop(out bullet)){
            bullet = Instantiate(bulletItem);
            bullet.onCollision += OnBulletCollision;
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
    void OnBulletCollision(BulletItem bullet, PlayerController target){
        bulletPool.Push(bullet);
        /*if(target == controller) return;
        bullet.gameObject.SetActive(false);
        if(target != null && target.Health.Damage(1)){
            controller.Score.AddPoint();
        }*/
    }
    
        
}

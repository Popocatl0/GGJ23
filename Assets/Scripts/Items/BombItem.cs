using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Handle the speed,direction and collision of the bullet 
/// </summary>
public class BombItem : CarryItem{
    public float speed;
    public float lifeCarry;
    public int explodeSize;
    public LayerMask m_LayerMask;
    Rigidbody2D rigbody;
    Collider2D colliderBomb;
    TextMeshPro counter;

    const int initBounce = 2;
    int currentBounce;
    int maxBounce;
    float lifeTimer = 0;
    Vector2 lastVelocity;
    float stopTime = 0;
    bool canHit = false;

    public delegate void OnCollisionDelegate(BombItem item, PlayerController target);
    public OnCollisionDelegate onDestroyCollision;


    void Awake(){
        rigbody = GetComponent<Rigidbody2D>();
        colliderBomb = GetComponent<Collider2D>();
        counter = GetComponentInChildren<TextMeshPro>();
    }
    /// <summary>
    /// Check its life time and destroy it
    ///
    /// </summary>
    void Update(){
        if(lifeTimer > 0){
            lifeTimer -= Time.deltaTime;
        }
        else if(lifeTimer <= 0){
            DelayExplode();
        }
        counter.text = Mathf.Ceil(lifeTimer).ToString("00");

        if(currentBounce >= maxBounce){
            if(stopTime > 0){
                stopTime -= Time.deltaTime;
            }
            if (stopTime <= 0){
                rigbody.velocity = Vector2.zero;
                rigbody.bodyType = RigidbodyType2D.Static;
                canHit = false;
            }
        }
    }

    public override void Init(Transform parent){
        this.gameObject.SetActive(true);
        this.transform.parent = parent;
        this.transform.position = parent.position;

        colliderBomb.enabled = false;
        currentBounce = 0;
        rigbody.velocity = Vector2.zero;
        rigbody.bodyType = RigidbodyType2D.Kinematic;
        lastVelocity = rigbody.velocity;
        stopTime = 0;
        maxBounce = initBounce;
        canHit = false;
        if(lifeTimer <= 0)
            lifeTimer = lifeCarry;
    }
    /// <summary>
    /// Initialize the direction and speed
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="pos"></param>
    public void Throw(Vector2 dir, Vector2 pos){
        this.transform.parent = null;
        this.gameObject.SetActive(true);
        rigbody.bodyType = RigidbodyType2D.Dynamic;
        this.transform.position = pos;
        rigbody.velocity = dir.normalized * speed;
        lastVelocity = rigbody.velocity;
        colliderBomb.enabled = true;
        StartCoroutine(DelayCollision());
    }

    IEnumerator DelayCollision()
    {
        yield return new WaitForSeconds(0.5f);
        canHit = true;
    }
    /// <summary>
    /// When collides with a player, call a delegate for handle the damage and score
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player" && rigbody.velocity.magnitude > 0 && canHit){
            other.gameObject.GetComponent<PlayerController>().Health.Damage(1);
            SimpleExplode();
        }
        else if(other.gameObject.tag == "Wall"){
            if (currentBounce >= maxBounce){
                rigbody.velocity = Vector2.zero;
                lastVelocity = rigbody.velocity;
            }
            Vector2 reflect = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);
            rigbody.velocity = reflect.normalized * speed;
            lastVelocity = rigbody.velocity;

            currentBounce++;
            if(currentBounce >= maxBounce){
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, reflect.normalized);

                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider.gameObject.tag == "Wall"){

                        float distance = Vector2.Distance(hit[i].point, transform.position) /2;
                        stopTime = distance / rigbody.velocity.magnitude;
                        break;
                    }
                }
            }
        }
    }

    public override CarryItem OnCarry(){
        return this;
    }

    public override void OnSet(PlayerController controller) {
        Throw(controller.currentDir, controller.Target.transform.position);
    }

    public override void OnCharge(CarryItem item){
        if(item.GetType() == typeof(Floweritem)){
            maxBounce++;
            item.OnCarry();
        }
    }
    public void DisableBomb() {
        rigbody.velocity = Vector2.zero;
        lifeTimer = 0;
        colliderBomb.enabled = false;
        this.transform.parent = null;
        this.gameObject.SetActive(false);
        onDestroyCollision(this, null);
    }

    void SimpleExplode(){
        rigbody.velocity = Vector2.zero;
        lifeTimer = 0;
        colliderBomb.enabled = false;
        this.transform.parent = null;
        //call feedback
        //delay
        this.gameObject.SetActive(false);
        onDestroyCollision(this, null);
    }

    void DelayExplode(){
        rigbody.velocity = Vector2.zero;
        lifeTimer = 0;
        colliderBomb.enabled = false;
        this.transform.parent = null;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, Vector2.one * this.explodeSize, 0, m_LayerMask);
        for (int i = 0; i < hitColliders.Length; i++){
            if (hitColliders[i].gameObject.tag == "Player"){
                hitColliders[i].gameObject.GetComponent<PlayerController>().Health.Damage(1);
                break;
            }
        }
        //feedback
        //delay
        this.gameObject.SetActive(false);
        onDestroyCollision(this, null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.transform.position, Vector2.one * this.explodeSize);
    }
}

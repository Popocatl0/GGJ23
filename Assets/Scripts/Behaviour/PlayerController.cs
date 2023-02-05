using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contain all the components of the ship
/// Manage the Actions Cycle
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputManager))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] string _id;
    [SerializeField] PlayerData _data;
    [SerializeField] InputManager _input;
    [SerializeField] Transform _carryPos;
    public Rigidbody2D Rigbody {get; private set;}
    public Collider2D Collider {get; private set;}
    public Health Health {get; private set;}
    public PlayerTarget Target {get; private set;}
    public SpriteRenderer Render {get; private set;}
    public Animator Animator {get; private set;}
    public bool isEnabled {get; private set;}
    public Vector2 currentDir {get; set;}

    #region GETTERS 
    public PlayerData Data{
        get{ return _data;}
    }

    public InputManager Input{
        get{ return _input;}
    }
    public String ID{
        get{ return _id;}
    }
    public Transform CarryPos{
        get{ return _carryPos;}
    }
    #endregion

    EntityAction[] actions;

    void Awake(){
        Rigbody = GetComponent<Rigidbody2D>();
        Health = GetComponent<Health>();
        Collider = GetComponent<Collider2D>();
        Target = GetComponentInChildren<PlayerTarget>();
        Animator = GetComponentInChildren<Animator>();
        Render = GetComponentInChildren<SpriteRenderer>();
        currentDir = Vector2.right;

        actions = GetComponents<EntityAction>();
        foreach(var act in actions){
            act.Init(this);
        }

        _input.SetController(this);
        Health.SetController(this);
        Target.SetController(this);
    }

    /// <summary>
    /// Enable or disable itself and its actions
    /// </summary>
    /// <param name="val"></param>
    public void SetEnabled(bool val=true){
        isEnabled = val;
        Rigbody.velocity = Vector2.zero;
        Collider.enabled = val;
        Animator.Play("Idle");
        foreach(var act in actions){
            act.SetActive(val);
            if(!val) act.StopAction();
        }
    }
    public void StopAction()
    {
        foreach (var act in actions){
            act.StopAction();
        }
    }
    /// <summary>
    /// When a input is received, call the respective action's method
    /// </summary>
    public void UpdateInput(){
        if(!isEnabled) return;
        foreach(var act in actions){
            if(act.actionEnabled) act.BeginAction();
        }
    }
    /// <summary>
    /// Every frame call the respective action's method
    /// </summary>
    void Update(){
        if(!isEnabled) return;
        foreach(var act in actions){
            if(act.actionEnabled) act.ProcessAction();
        }
    }
    /// <summary>
    /// Every fixed cycle call the respective action's method
    /// </summary>
    void FixedUpdate(){
        if(!isEnabled) return;
        foreach(var act in actions){
            if(act.actionEnabled) act.ProcessFixedAction();
        }
    }
    /// <summary>
    /// Reset all components of the ship and enable it
    /// </summary>
    /// <param name="resetPos"></param>
    public void ResetObject(){
        gameObject.SetActive(true);
        Health.Revive();
        SetEnabled();
    }
}

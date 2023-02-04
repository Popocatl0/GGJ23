using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "playerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Player")]
    [Tooltip("Maximum speed for the player.")]
    public float maxSpeed;
    [Tooltip("Initial life at the start of each match.")]
    public int maxLife;

    [Header("Target")]
    [Tooltip("Size of the box trigger")]
    public Vector2 targetSize;
    [Tooltip("Distance of the target")]
    public float targetDistance;
}


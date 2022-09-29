using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerState
{
    public int healthPoint;
    public float moveSpeed;
    public float rotSpeed;
}

public class Player : MonoBehaviour
{
    public PlayerState state { get; private set; }

    private void Awake()
    {
        state = new PlayerState { healthPoint = 100, moveSpeed = 1f, rotSpeed = 3f };
    }
}
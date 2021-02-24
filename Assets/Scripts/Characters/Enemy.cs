using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action onPlayerKill;
    [SerializeField] private float _movementSpeed;
    private bool _monsterAwake = false ;
    [SerializeField] private Vector2 _startPosition;

    private void OnEnable() {
        CharacterController.OnPlayerMoveDone += AwakeMonster;
    }
    private void OnDisable() {
        CharacterController.OnPlayerMoveDone -= AwakeMonster;
    }

    private void AwakeMonster() => _monsterAwake = true;
    private void FixedUpdate() {
        if (_monsterAwake)
            Move();
    }

    private void Move()
    {
        Vector2 movement = (Vector2) transform.position + Vector2.up * _movementSpeed*Time.fixedDeltaTime;
        transform.position = movement;
    }

    public void SpeedUp(float amount) //for debug
    {
        _movementSpeed += amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("death");
            onPlayerKill?.Invoke();
        }
    }
}

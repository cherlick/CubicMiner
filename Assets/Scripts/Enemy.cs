using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action onPlayerKill;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _stepsMovement;
    [SerializeField] private Vector2 _startPosition;

    private void FixedUpdate() {
        MoveWithSpeed();

        if (transform.position.y>2)
        {
            transform.position = _startPosition;
        }
    }

    private void MoveWithSpeed()
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

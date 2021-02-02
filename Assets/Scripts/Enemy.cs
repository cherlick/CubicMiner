using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool _isMovementBytime;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _stepsMovement;
    [SerializeField] private Vector2 _startPosition;

    private void Update() {
        if (_isMovementBytime)
        {
            MoveWithSpeed();
        }
    }

    private void MoveWithSpeed()
    {
        Vector2 movement = (Vector2) transform.position + Vector2.up * _movementSpeed*Time.fixedDeltaTime;
        transform.position = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("death");
        }
    }
}

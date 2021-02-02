using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CastMyWay;
using MobileInputSystem;
using HealthCareSystem;

public class CharacterController : MonoBehaviour
{
    /*
    get the inputs of the player and send info to move or attack to the correct dirrection 
    */
    private RayCastSystem<Transform> _rayCast = new RayCastSystem<Transform>();
    private MobileInputs _getInputs = null;
    private bool _moveInProgress;

    private void Awake() {
        _getInputs = FindObjectOfType<MobileInputs>();
    }

    private void Update()
    {
        HandleInput();

        #region Test
        if (Input.GetKeyDown(KeyCode.A))
            Move(Vector2.left);
        if (Input.GetKeyDown(KeyCode.D))
            Move(Vector2.right);
        if (Input.GetKeyDown(KeyCode.W))
            Move(Vector2.up);
        if (Input.GetKeyDown(KeyCode.S))
            Move(Vector2.down);

        #endregion
    }

    private void HandleInput()
    {
        Vector2 direction = Vector2.zero;
        if (_moveInProgress) return;

        if (_getInputs.IsSwipe)
            direction = _getInputs.SwipeDirection;//Using swipe to attack or move
            
        else if (_getInputs.IsTapTouch)
        {
            //Avoid Taping it self
            Transform objecttarget = _rayCast.GetObjectDetection(_getInputs.TouchPosition, transform.position, 0f, (1 << LayerMask.NameToLayer("Character")));
            if (objecttarget != null && objecttarget == transform)
                return;
            direction = _getInputs.TouchPosition - (Vector2)transform.position;
            Debug.Log(direction.magnitude);
            if (direction.magnitude > .77f) return;
            else direction = direction.normalized;

            Vector2 absDirection = new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));

            if (absDirection.x > 0.5f && absDirection.y > 0.5) // To Avoid moving whe tapping Diagonals 
                direction = Vector2.zero;

            if (absDirection.x > absDirection.y) direction.y = 0; //Zero out Y to move only in X
            else direction.x = 0; //Zero out X to move only in Y
            
        }
        else return;
        
        //Debug.Log(direction);
        if (direction.y <-0.4f || direction == Vector2.zero) return; // character cannot go down direction move to same place

        HandleDecision(direction);
        
    }

    private void HandleDecision(Vector2 direction)
    {
        _moveInProgress = true;
        _rayCast._rayCastDebugMode = true;
        Transform objecttarget = _rayCast.GetObjectDetection(transform.position, direction, 0.5f,~(1 << LayerMask.NameToLayer("Character")));
        
        if (objecttarget!=null)
            Attack(direction);
        else if(objecttarget!= transform)
            Move(direction);
        else _moveInProgress = false;
        
        //Debug.Log(objecttarget + " Dir");
    }
    
    private void Move(Vector2 direction)
    {
        /*
        move to same direction
        */

        transform.Translate(direction.normalized / 2);
        //Debug.Log("Move");
        Invoke("ResetVars",0.2f); //To avoid multiple moves in one swipe or tap
    }

    private void ResetVars() {
        _moveInProgress = false;
    }
    private void Attack(Vector2 direction)
    {
        /*
        attack to same direction
        */
        Debug.Log("Attack");
        HealthSystem objecttarget = _rayCast.GetObjectDetection(transform.position, direction, 0.5f, ~(1 << LayerMask.NameToLayer("Character"))).gameObject.GetComponentInChildren<HealthSystem>();
        objecttarget?.TakeDamage(1);
        _moveInProgress = false;
    }
}
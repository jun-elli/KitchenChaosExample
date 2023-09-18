using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speedMultiplier = 7f;
    private float _rotationSpeed = 10f;
    public bool IsWalking { get; private set; }


    private void Update()
    {
        ControllMovement();
    }

    private void ControllMovement()
    {

        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        // Player is Idle
        IsWalking = false;

        // If there's movement:
        if (inputVector != Vector2.zero)
        {
            // Player is walking
            IsWalking = true;


            // Add vector to actual position with speed
            Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);
            transform.position += moveDirection * _speedMultiplier * Time.deltaTime;

            // Add rotation to make z point to our moving direction
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * _rotationSpeed);
        }

    }


}

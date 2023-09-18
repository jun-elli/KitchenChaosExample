using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speedMultiplier = 7f;
    private float _rotationSpeed = 10f;
    public bool IsWalking { get; private set; }
    private const float PLAYER_HEIGHT = 2f;
    private const float PLAYER_RADIUS = 0.7f;

    private void Update()
    {
        ControlMovement();
    }

    private void ControlMovement()
    {
        // Player is Idle
        IsWalking = false;

        // Get input and vector to move
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);

        // Check if we can move in that direction
        Vector3 bottomPoint = transform.position;
        Vector3 topPoint = transform.position + Vector3.up * PLAYER_HEIGHT;
        // Max distance it wants to move
        float moveDistance = _speedMultiplier * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(bottomPoint, topPoint, PLAYER_RADIUS, moveDirection, moveDistance);
        bool wantsToMove = inputVector != Vector2.zero;

        if (!canMove)
        {
            // Check if we can move on the x
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            bool canMoveX = !Physics.CapsuleCast(bottomPoint, topPoint, PLAYER_RADIUS, moveDirectionX, moveDistance);

            if (canMoveX)
            {
                moveDirection = moveDirectionX;
                canMove = true;
            }
            else
            {
                // Check if we can move on the z
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                bool canMoveZ = !Physics.CapsuleCast(bottomPoint, topPoint, PLAYER_RADIUS, moveDirectionZ, moveDistance);

                if (canMoveZ)
                {
                    moveDirection = moveDirectionZ;
                    canMove = true;
                }
            }
        }

        // If there's movement:
        if (wantsToMove && canMove)
        {
            // Player is walking
            IsWalking = true;

            // Add vector to actual position with speed
            transform.position += moveDirection * moveDistance;

            // Add rotation to make z point to our moving direction
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * _rotationSpeed);
        }

    }



}

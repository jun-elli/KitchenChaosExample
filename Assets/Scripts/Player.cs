using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 7f;
    private float rotationSpeed = 10f;


    private void Update()
    {
        ControllMovement();
    }

    private void ControllMovement()
    {
        Vector2 input = new(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            input.y += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.y -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.x -= 1f;
        }

        input = input.normalized;
        // Add vector to actual position with speed
        Vector3 moveDirection = new(input.x, 0f, input.y);
        transform.position += moveDirection * speedMultiplier * Time.deltaTime;

        // Add rotation to make z point to our moving direction
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

    }
}

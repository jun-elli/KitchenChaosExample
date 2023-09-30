using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }

    // Events
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }


    [SerializeField] private float _speedMultiplier = 7f;
    [SerializeField] private LayerMask _countersLayerMask;
    public bool IsWalking { get; private set; }

    private Vector3 _lastInteractDirection;
    private ClearCounter _selectedCounter;

    // Constants
    private const float ROTATION_SPEED = 15f;
    private const float PLAYER_HEIGHT = 2f;
    private const float PLAYER_RADIUS = 0.7f;
    private const float INTERACT_DISTANCE = 2f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Add method to handle interaction with a counter to delegate Interact (E)
        GameInput.Instance.OnInteractAction += GameInput_OnInteraction;
    }

    // Method that holds actions to happen when Interact (E) is pressed
    private void GameInput_OnInteraction(object sender, EventArgs e)
    {
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact();
        }
    }

    private void Update()
    {
        ControlMovement();
        HandleInteractions();
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
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * ROTATION_SPEED);
        }

    }

    private void HandleInteractions()
    {
        // Get input and vector to move
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);

        // Save last direction 'cause if we stop, we'll need the reference
        if (moveDirection != Vector3.zero)
        {
            _lastInteractDirection = moveDirection;
        }

        // Look what we hit in that direction
        if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, INTERACT_DISTANCE, _countersLayerMask))
        {
            // Check for Clear Counter
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != _selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }



    }

    private void SetSelectedCounter(ClearCounter counter)
    {
        _selectedCounter = counter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = _selectedCounter
        });
    }

}

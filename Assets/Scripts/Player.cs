using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; }

    // Events
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    public event Action OnPickUpSomething;


    [SerializeField] private float _speedMultiplier = 7f;
    [SerializeField] private LayerMask _countersLayerMask;
    public bool IsWalking { get; private set; }
    private bool isMovementLocked;
    private float movementLockedTimer;

    private Vector3 _lastInteractDirection;
    private BaseCounter _selectedCounter;

    [SerializeField] private Transform holdingObjectPoint;
    private KitchenObject kitchenObject;

    // Constants
    private const float ROTATION_SPEED = 18f;
    private const float PLAYER_HEIGHT = 2f;
    private const float PLAYER_RADIUS = 0.7f;
    private const float INTERACT_DISTANCE = 2f;
    private const float MIN_INPUT_TO_ALLOW_MOVEMENT = 0.5f;

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
        // Add method to handle interaction with a counter to delegate Interact (F)
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnAlternateInteraction;
    }

    private void GameInput_OnAlternateInteraction(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying)
        {
            if (_selectedCounter != null)
            {
                _selectedCounter.InteractAlternate(this);
            }
        }
    }

    // Method that holds actions to happen when Interact (E) is pressed
    private void GameInput_OnInteraction(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying)
        {
            if (_selectedCounter != null)
            {
                _selectedCounter.Interact(this);
            }
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

        // Check if movement is locked and wait
        if (movementLockedTimer > 0)
        {
            movementLockedTimer -= Time.deltaTime;
            return;
        }
        isMovementLocked = false;

        // Get input and vector to move
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);

        // Get player size
        Vector3 bottomPoint = transform.position;
        Vector3 topPoint = transform.position + Vector3.up * PLAYER_HEIGHT;
        // Max distance it wants to move
        float moveDistance = _speedMultiplier * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(bottomPoint, topPoint, PLAYER_RADIUS, moveDirection, moveDistance, _countersLayerMask);
        bool wantsToMove = inputVector != Vector2.zero;

        if (!canMove)
        {
            // Check if we can move on the x
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            bool canMoveX = (moveDirection.x < -MIN_INPUT_TO_ALLOW_MOVEMENT || moveDirection.x > MIN_INPUT_TO_ALLOW_MOVEMENT) && !Physics.CapsuleCast(bottomPoint, topPoint, PLAYER_RADIUS, moveDirectionX, moveDistance);

            if (canMoveX)
            {
                moveDirection = moveDirectionX;
                canMove = true;
            }
            else
            {
                // Check if we can move on the z
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                bool canMoveZ = (moveDirection.z < -MIN_INPUT_TO_ALLOW_MOVEMENT || moveDirection.z > MIN_INPUT_TO_ALLOW_MOVEMENT) && !Physics.CapsuleCast(bottomPoint, topPoint, PLAYER_RADIUS, moveDirectionZ, moveDistance);

                if (canMoveZ)
                {
                    moveDirection = moveDirectionZ;
                    canMove = true;
                }
            }
        }

        // If player wants to move
        if (wantsToMove)
        {
            // Player is walking (even stationary)
            IsWalking = true;

            if (canMove)
            {
                // Add vector to actual position with speed
                transform.position += moveDirection * moveDistance;
            }

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
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != _selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
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

    private void SetSelectedCounter(BaseCounter counter)
    {
        _selectedCounter = counter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = _selectedCounter
        });
    }

    public Transform GetParentHoldingPoint()
    {
        return holdingObjectPoint;
    }

    public void SetKitchenObject(KitchenObject obj)
    {
        kitchenObject = obj;
        if (kitchenObject != null)
        {
            OnPickUpSomething?.Invoke();
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public void LockMovementForSeconds(float seconds)
    {
        isMovementLocked = true;
        movementLockedTimer = seconds;
    }
}

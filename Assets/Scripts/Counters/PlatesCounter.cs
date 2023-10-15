using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateSO;

    private float spawnPlateTimer;
    private int currentPlateAmount;
    private const float SPAWN_DELAY = 4f;
    private const int MAX_PLATES = 3;

    // events
    public event Action OnPlateSpawned;
    public event Action OnPlateRemoved;

    private void Update()
    {
        HandlePlateSpawning();
    }

    private void HandlePlateSpawning()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > SPAWN_DELAY)
        {
            spawnPlateTimer = 0f;
            if (GameManager.Instance.IsGamePlaying && currentPlateAmount < MAX_PLATES)
            {
                currentPlateAmount++;

                OnPlateSpawned?.Invoke();
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (currentPlateAmount > 0)
            {
                currentPlateAmount--;

                KitchenObject.SpawnKitchenObject(plateSO, player);

                OnPlateRemoved?.Invoke();
            }
        }
    }
}

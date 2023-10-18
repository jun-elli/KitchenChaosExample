using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTile : MonoBehaviour
{
    [SerializeField] private TeleportTile destinationTile;

    public bool isTeleporting = true;
    private const float MOVEMENT_DELAY_AFTER_TELEPORT = 0.3f;

    // Events
    public static event Action<float> OnPlayerTeleported;
    public static event Action<TeleportTile> OnTeleportActivated;
    public static void ResetStaticData()
    {
        OnPlayerTeleported = null;
        OnTeleportActivated = null;
    }
    public event Action OnPlayerEnterTile;

    private void OnTriggerEnter(Collider other)
    {
        if (destinationTile != null && isTeleporting)
        {
            destinationTile.isTeleporting = false;
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.LockMovementForSeconds(MOVEMENT_DELAY_AFTER_TELEPORT);
                OnPlayerEnterTile?.Invoke();
                OnTeleportActivated?.Invoke(this);
                OnPlayerTeleported?.Invoke(MOVEMENT_DELAY_AFTER_TELEPORT);
                player.transform.position = destinationTile.transform.position;
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        isTeleporting = false;
    }

    private void OnTriggerExit(Collider other)
    {
        isTeleporting = true;
    }

}

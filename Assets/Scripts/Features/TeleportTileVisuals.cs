using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTileVisuals : MonoBehaviour
{
    [SerializeField] private TeleportTile tile;
    [SerializeField] private ParticleSystem circleParticleSystem;

    private void Start()
    {
        tile.OnPlayerEnterTile += TeleportTile_OnPlayerEnterTile;
    }

    private void TeleportTile_OnPlayerEnterTile()
    {
        circleParticleSystem.Play();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem movementParticleSystem;

    // Animator parameters
    private const string IS_WALKING = "IsWalking";

    private Animator animator;
    private float particlesPausedTimer;
    private bool areParticlesPaused;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        TeleportTile.OnPlayerTeleported += TeleportTile_OnPlayerTeleported;
        movementParticleSystem.Play();
    }

    private void TeleportTile_OnPlayerTeleported(float secondsToPause)
    {
        movementParticleSystem.Stop();
        particlesPausedTimer = secondsToPause;
        areParticlesPaused = true;
    }

    private void Update()
    {
        if (areParticlesPaused)
        {
            particlesPausedTimer -= Time.deltaTime;
        }
        if (particlesPausedTimer < 0 && areParticlesPaused)
        {
            areParticlesPaused = false;
            movementParticleSystem.Play();
        }
        animator.SetBool(IS_WALKING, player.IsWalking);
    }
}

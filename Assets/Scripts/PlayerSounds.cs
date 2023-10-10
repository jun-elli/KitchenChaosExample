using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;

    private float footstepsTimer;
    private const float FOOTSTEPS_DELAY = 0.1f;
    private const float FOOTSTEPS_VOLUME = 1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        HandleFootstepsSound();
    }

    private void HandleFootstepsSound()
    {
        footstepsTimer -= Time.deltaTime;
        if (footstepsTimer < 0)
        {
            footstepsTimer = FOOTSTEPS_DELAY;

            if (player.IsWalking)
            {
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, FOOTSTEPS_VOLUME);
            }
        }
    }
}

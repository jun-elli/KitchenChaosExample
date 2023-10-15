using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private bool playWarningSound;
    private const float GOING_TO_BURN_WARNING_THRESHOLD = 0.5f;
    private float warningSoundTimer;
    private const float WARNING_SOUND_DELAY = 0.3f;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0)
            {
                warningSoundTimer = WARNING_SOUND_DELAY;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }

    private void StoveCounter_OnProgressChanged(float normalizedProgress)
    {
        playWarningSound = normalizedProgress >= GOING_TO_BURN_WARNING_THRESHOLD && stoveCounter.HasFriedState;

    }

    private void StoveCounter_OnStateChanged(StoveCounter.StoveState state)
    {
        bool playSound = state == StoveCounter.StoveState.Frying || state == StoveCounter.StoveState.Fried;

        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}

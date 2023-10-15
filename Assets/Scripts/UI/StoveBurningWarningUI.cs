using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurningWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private const float GOING_TO_BURN_WARNING_THRESHOLD = 0.5f;

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        Hide();
    }

    private void StoveCounter_OnProgressChanged(float normalizedProgress)
    {
        bool show = normalizedProgress >= GOING_TO_BURN_WARNING_THRESHOLD && stoveCounter.HasFriedState;

        if (show)
        {
            Show();
        }
        else
        {
            Hide();
        }

    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}



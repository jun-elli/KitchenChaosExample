using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnCounter;
    [SerializeField] private GameObject particles;

    private StoveCounter stove;

    private void Start()
    {
        stove = GetComponentInParent<StoveCounter>();
        stove.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(StoveCounter.StoveState state)
    {
        bool isActive = state == StoveCounter.StoveState.Frying || state == StoveCounter.StoveState.Fried;
        ShowOrHide(isActive);
    }

    private void ShowOrHide(bool isActive)
    {
        stoveOnCounter.SetActive(isActive);
        particles.SetActive(isActive);
    }
}

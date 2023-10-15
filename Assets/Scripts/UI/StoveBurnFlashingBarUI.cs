using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private Animator animator;
    private const float GOING_TO_BURN_WARNING_THRESHOLD = 0.5f;
    private const string IS_FLASHING_ANIM_BOOL = "isFlashing";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_FLASHING_ANIM_BOOL, false);

    }

    private void StoveCounter_OnProgressChanged(float normalizedProgress)
    {
        bool flash = normalizedProgress >= GOING_TO_BURN_WARNING_THRESHOLD && stoveCounter.HasFriedState;

        animator.SetBool(IS_FLASHING_ANIM_BOOL, flash);


    }


}

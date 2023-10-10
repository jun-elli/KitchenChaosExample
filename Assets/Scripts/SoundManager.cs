using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefSO clipRefsSO;

    // Positions
    [SerializeField] private Transform deliveryCounter;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManager_OnRecipeDelivered;
        DeliveryManager.Instance.OnWrongRecipeDelivered += DeliveryManager_OnWrongRecipeDelivered;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickUpSomething += Player_OnPickUpSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(TrashCounter counter)
    {
        PlaySound(clipRefsSO.trash, counter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(BaseCounter counter)
    {
        PlaySound(clipRefsSO.objectDrop, counter.transform.position);
    }

    private void Player_OnPickUpSomething()
    {
        PlaySound(clipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(CuttingCounter counter)
    {
        PlaySound(clipRefsSO.chop, counter.transform.position);
    }

    private void DeliveryManager_OnRecipeDelivered()
    {
        PlaySound(clipRefsSO.deliverySuccess, deliveryCounter.position);
    }

    private void DeliveryManager_OnWrongRecipeDelivered()
    {
        PlaySound(clipRefsSO.deliveryFail, deliveryCounter.position);
    }

    private void PlaySound(AudioClip[] clipArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clipArray[UnityEngine.Random.Range(0, clipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip clip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume = 1f)
    {
        PlaySound(clipRefsSO.footstep, position, volume);
    }
}

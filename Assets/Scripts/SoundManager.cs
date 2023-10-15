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

    public float Volume { get; private set; }

    private const string PLAYER_PREFS_SFX_VOLUME = "SfxVolume";

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
        Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRightRecipeDelivered += DeliveryManager_OnRecipeDelivered;
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

    private void PlaySound(AudioClip[] clipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(clipArray[UnityEngine.Random.Range(0, clipArray.Length)], position, volumeMultiplier * Volume);
    }

    private void PlaySound(AudioClip clip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(clip, position, volumeMultiplier * Volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(clipRefsSO.footstep, position, volumeMultiplier * Volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(clipRefsSO.warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(clipRefsSO.warning, position);
    }

    public void ChangeVolume()
    {
        Volume += 0.1f;
        if (Volume > 1.09f)
        {
            Volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, Volume);
        PlayerPrefs.Save();
    }
}

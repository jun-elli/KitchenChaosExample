using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;
    public float Volume { get; private set; }
    private const float DEFAULT_VOLUME = 0.3f;
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";


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
        Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, DEFAULT_VOLUME);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = Volume;
    }

    public void ChangeVolume()
    {
        Volume += 0.1f;
        if (Volume > 1.09f)
        {
            Volume = 0f;
        }
        audioSource.volume = Volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, Volume);
        PlayerPrefs.Save();
    }
}

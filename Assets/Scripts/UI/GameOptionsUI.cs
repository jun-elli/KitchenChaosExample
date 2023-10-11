
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsUI : MonoBehaviour
{

    public static GameOptionsUI Instance { get; private set; }

    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI sfxButtonText;
    [SerializeField] private TextMeshProUGUI musicButtonText;
    private const string SFX_VOLUME_TEXT = "SFX Volume: ";
    private const string MUSIC_VOLUME_TEXT = "Music Volume: ";

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

        sfxButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateSFXVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateMusicVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePauseToggled += GameManager_OnGamePauseToggled;

        UpdateSFXVisual();
        UpdateMusicVisual();
        Hide();
    }

    private void GameManager_OnGamePauseToggled(bool isGamePaused)
    {
        if (!isGamePaused)
        {
            Hide();
        }
    }

    private void UpdateSFXVisual()
    {
        sfxButtonText.text = SFX_VOLUME_TEXT + Mathf.Round(SoundManager.Instance.Volume * 10f);
    }

    private void UpdateMusicVisual()
    {
        musicButtonText.text = MUSIC_VOLUME_TEXT + Mathf.Round(MusicManager.Instance.Volume * 10f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

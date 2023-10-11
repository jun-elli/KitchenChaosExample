
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

    // Key Binding
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Transform pressToRebindScreen;

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
        // Rebinding
        moveUpButton.onClick.AddListener(() => { HandleRebindBinding(GameInput.Binding.MoveUp); });
        moveDownButton.onClick.AddListener(() => { HandleRebindBinding(GameInput.Binding.MoveDown); });
        moveLeftButton.onClick.AddListener(() => { HandleRebindBinding(GameInput.Binding.MoveLeft); });
        moveRightButton.onClick.AddListener(() => { HandleRebindBinding(GameInput.Binding.MoveRight); });
        interactButton.onClick.AddListener(() => { HandleRebindBinding(GameInput.Binding.Interact); });
        interactAlternateButton.onClick.AddListener(() => { HandleRebindBinding(GameInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => { HandleRebindBinding(GameInput.Binding.Pause); });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePauseToggled += GameManager_OnGamePauseToggled;

        UpdateSFXVisual();
        UpdateMusicVisual();
        UpdateKeyBindingVisual();
        Hide();
        HidePressToRebindScreen();
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
    private void UpdateKeyBindingVisual()
    {
        moveUpButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        moveDownButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        moveLeftButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        moveRightButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        interactButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseButton.GetComponentInChildren<TextMeshProUGUI>().text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowPressToRebindScreen()
    {
        pressToRebindScreen.gameObject.SetActive(true);
    }

    private void HidePressToRebindScreen()
    {
        pressToRebindScreen.gameObject.SetActive(false);
    }

    private void HandleRebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindScreen();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindScreen();
            UpdateKeyBindingVisual();
        });
    }
}

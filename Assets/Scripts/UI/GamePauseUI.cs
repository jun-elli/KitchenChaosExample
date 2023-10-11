using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button returnToTitleButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        optionsButton.onClick.AddListener(() =>
        {
            GameOptionsUI.Instance.Show();
        });
        returnToTitleButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
            Loader.Load(Loader.Scene.MainTitleScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePauseToggled += GameManager_OnGamePauseToggled;

        Hide();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGamePauseToggled -= GameManager_OnGamePauseToggled;
    }

    private void GameManager_OnGamePauseToggled(bool isGamePaused)
    {
        if (isGamePaused)
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        Hide();
    }

    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.GamePlaying)
        {
            Show();
            StartCoroutine(UpdateGamePlayingTimerVisual());
        }
        else
        {
            Hide();
        }
    }

    private IEnumerator UpdateGamePlayingTimerVisual()
    {
        float currentPlayingTimer = GameManager.Instance.GetGamePlayingTimerNormalized();
        Debug.Log(currentPlayingTimer);
        while (currentPlayingTimer < 1)
        {
            currentPlayingTimer = GameManager.Instance.GetGamePlayingTimerNormalized();
            Debug.Log(currentPlayingTimer);
            timerImage.fillAmount = currentPlayingTimer;
            yield return null;
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

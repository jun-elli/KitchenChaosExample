using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;

        Hide();
    }

    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.CountdownToStart)
        {
            Show();
            StartCoroutine(UpdateCountdownText());
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

    private IEnumerator UpdateCountdownText()
    {
        float currentTime = Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer());
        while (currentTime > 0)
        {
            currentTime = Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer());
            countdownText.text = currentTime.ToString();
            yield return new WaitForSeconds(0.3f);
        }
    }
}

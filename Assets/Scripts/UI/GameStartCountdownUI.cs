using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    private Animator animator;
    private const string NUMBER_POPUP_TRIGGER = "NumberPopup";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
        countdownText.text = "";
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator UpdateCountdownText()
    {
        int currentTime = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
        while (currentTime > 0)
        {
            currentTime = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
            countdownText.text = currentTime.ToString();
            animator.SetTrigger(NUMBER_POPUP_TRIGGER);
            SoundManager.Instance.PlayCountdownSound();
            yield return new WaitForSeconds(1f);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public enum GameState
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private GameState state;
    private float waitingToStartTimer;
    private const float WAITING_TO_START_DELAY = 1f;
    private float _countdownToStartTimer;
    private const float COUNTDOWN_TIME_MAX = 3f;
    private float gamePlayingTimer;
    private const float PLAYING_TIME_MAX = 10f;

    public bool IsGamePlaying => state == GameState.GamePlaying;

    // Events
    public event Action<GameState> OnGameStateChanged;


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

        state = GameState.WaitingToStart;
        waitingToStartTimer = WAITING_TO_START_DELAY;
        _countdownToStartTimer = COUNTDOWN_TIME_MAX;
        gamePlayingTimer = PLAYING_TIME_MAX;
    }

    private void Update()
    {
        HandleGameState();
    }

    private void HandleGameState()
    {
        switch (state)
        {
            case GameState.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0)
                {
                    state = GameState.CountdownToStart;
                    OnGameStateChanged?.Invoke(state);
                }
                break;

            case GameState.CountdownToStart:
                _countdownToStartTimer -= Time.deltaTime;
                if (_countdownToStartTimer < 0)
                {
                    state = GameState.GamePlaying;
                    OnGameStateChanged?.Invoke(state);
                }
                break;

            case GameState.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0)
                {
                    state = GameState.GameOver;
                    OnGameStateChanged?.Invoke(state);
                }
                break;

            case GameState.GameOver:
                break;
        }
    }

    public float GetCountdownToStartTimer()
    {
        return _countdownToStartTimer;
    }
}

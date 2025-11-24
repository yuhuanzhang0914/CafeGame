using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public static GameManager Instance { get; private set; }


    public event EventHandler OnStateChanged;

  
    public enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    [SerializeField] private Player player;

    private State state;


    [SerializeField] private float waitingToStartTimer = 1f;
    [SerializeField] private float countDownToStartTimer = 3f;
    [SerializeField] private float gamePlayingTimer = 60f;
    private bool isGamePause = false;

    private void Awake()
    {

   
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
     
    }

    private void Start()
    {
        TurnToWaitingToStart();
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;

    }
    private void GameInput_OnPauseAction(object sender,EventArgs e)
    {
        ToggleGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0f)
                {
                    TurnToCountDownToStart();
                }
                break;

            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer <= 0f)
                {
                    TurnToGamePlaying();
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0f)
                {
                    TurnToGameOver();
                }
                break;

            case State.GameOver:
               
                break;
        }
    }

    private void TurnToWaitingToStart()
    {
        state = State.WaitingToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToCountDownToStart()
    {
        state = State.CountDownToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToGamePlaying()
    {
        state = State.GamePlaying;
        EnablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToGameOver()
    {
        state = State.GameOver;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void DisablePlayer()
    { 
            player.enabled = false;  
    }

    private void EnablePlayer()
    {
            player.enabled = true; 
    }

    public bool IsCountDownState()
    {
        return state == State.CountDownToStart;
    }

    public bool IsGamePlayingState()
    {
        return state == State.GamePlaying;
    }

    public bool IsGameOverState()
    {
        return state == State.GameOver;
    }

    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer;
    }

 
    public State GetState()
    {
        return state;
    }
    private void ToggleGame()
    {
        isGamePause = !isGamePause;
        if(isGamePause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
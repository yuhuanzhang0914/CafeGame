using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }
    [SerializeField] private Player player;

   private State state;

    private float waitingToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private float gamePlayingTimer = 10f;

    void Awake()
    {
        TurnToWaitingToStart();
    }

    void Update()
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
    }
    private void TurnToCountDownToStart()
    {
        state = State.CountDownToStart;
        DisablePlayer();
    }

    private void TurnToGamePlaying()
    {
        state = State.GamePlaying;
        EnablePlayer();
    }

    private void TurnToGameOver()
    {
        state = State.GameOver;
    }
    private void DisablePlayer()
    {
        player.enabled = false;
    } 
    private void EnablePlayer()
    {
        player.enabled= true;
    }
}
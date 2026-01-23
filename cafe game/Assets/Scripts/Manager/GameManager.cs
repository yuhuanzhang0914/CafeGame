using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

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
    private float gamePlayingTimeTotal;

    private bool isGamePaused = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        gamePlayingTimeTotal = gamePlayingTimer;
    }

    private void Start()
    {
        TurnToWaitingToStart();

        // ¼àÌýÔÝÍ£ÊäÈë£¨GameInput Àï´¥·¢ OnPauseAction£©
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        // °´Ò»´ÎÔÝÍ£ / ÔÙ°´Ò»´Î¼ÌÐø
        ToggleGame();
    }

    private void Update()
    {
        if (isGamePaused)
        {
            // ÔÝÍ£Ê±²»ÔÙ¸üÐÂ¼ÆÊ±Æ÷ºÍ×´Ì¬»ú
            return;
        }

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

    public void TurnToCountDownToStart()
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
        if (player != null)
        {
            player.enabled = false;
        }
    }

    private void EnablePlayer()
    {
        if (player != null)
        {
            player.enabled = true;
        }
    }

    // ========= ÔÝÍ£Ïà¹Ø =========

    public void ToggleGame()
    {
        if (isGamePaused)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }

    private void UnpauseGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        OnGameUnpaused?.Invoke(this, EventArgs.Empty);
    }

    // ========= ×´Ì¬²éÑ¯ =========

    // ¹© UITutorialUI µÈ½Å±¾µ÷ÓÃ
    public bool IsWaitingToStartState()
    {
        return state == State.WaitingToStart;
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

    // ========= ¶ÔÍâÌá¹©µÄ¼ÆÊ±ÐÅÏ¢ =========

    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer;
    }

    public State GetState()
    {
        return state;
    }

    public float GetGamePlayingTimer()
    {
        return gamePlayingTimer;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return gamePlayingTimer / gamePlayingTimeTotal;
    }
}
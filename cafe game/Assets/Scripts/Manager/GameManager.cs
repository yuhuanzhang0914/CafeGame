using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        // Listen for pause input
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGame();
    }

    private void Update()
    {
        if (isGamePaused) return;

        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0f)
                    TurnToCountDownToStart();
                break;

            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer <= 0f)
                    TurnToGamePlaying();
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0f)
                    TurnToGameOver();
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
    public static bool didPlayerDoWellStatic;

    private void TurnToGameOver()
    {
        state = State.GameOver;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);

        int successful = OrderManager.Instance.GetSuccessDeliveryCount();
        int threshold = 3;
        didPlayerDoWellStatic = successful >= threshold;

        // Load the ending scene
        SceneManager.LoadScene("EndingScene");
    }

    private void DisablePlayer()
    {
        if (player != null)
            player.enabled = false;
    }

    private void EnablePlayer()
    {
        if (player != null)
            player.enabled = true;
    }

    // ========= Pause / Unpause =========
    public void ToggleGame()
    {
        if (isGamePaused) UnpauseGame();
        else PauseGame();
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

    // ========= State Checks =========
    public bool IsWaitingToStartState() => state == State.WaitingToStart;
    public bool IsCountDownState() => state == State.CountDownToStart;
    public bool IsGamePlayingState() => state == State.GamePlaying;
    public bool IsGameOverState() => state == State.GameOver;

    // ========= Timers =========
    public float GetCountDownToStartTimer() => countDownToStartTimer;
    public State GetState() => state;
    public float GetGamePlayingTimer() => gamePlayingTimer;
    public float GetGamePlayingTimerNormalized() => gamePlayingTimer / gamePlayingTimeTotal;
}

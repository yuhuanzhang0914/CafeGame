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

    private bool isGamePaused = false;

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

        // 监听暂停输入（GameInput 里触发 OnPauseAction）
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        // 按一次暂停 / 再按一次继续
        ToggleGame();
    }

    private void Update()
    {
        if (isGamePaused)
        {
            // 暂停时不再更新计时器和状态机
            return;
        }

        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.unscaledDeltaTime;
                if (waitingToStartTimer <= 0f)
                {
                    TurnToCountDownToStart();
                }
                break;

            case State.CountDownToStart:
                countDownToStartTimer -= Time.unscaledDeltaTime;
                if (countDownToStartTimer <= 0f)
                {
                    TurnToGamePlaying();
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.unscaledDeltaTime;
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

    // ========= 暂停相关 =========

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

    // ========= 对外查询接口 =========

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
}
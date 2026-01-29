using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
   
    [SerializeField] private GameObject uiParent;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button settingsButton;
   

    private void Start()
    {
        Hide();

        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        // Resume °´Å¥
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleGame();
        });

        // ·µ»ØÖ÷²Ëµ¥°´Å¥
        menuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameMenu);
        });
        settingsButton.onClick.AddListener(() =>
        {
            SettingsUI.Instance.Show();
        });
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private  void Show()
    {
        uiParent.SetActive(true);
    }

    private void Hide()
    {
        uiParent.SetActive(false);
    }
}
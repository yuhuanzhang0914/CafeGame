using System;
using UnityEngine;

public class UITutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;   // 拖 Tutorial 整个 UI 的根对象进来

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        // 一开始游戏在 WaitingToStart 状态，一般是显示教程
        if (GameManager.Instance.IsWaitingToStartState())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsWaitingToStartState())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        uiParent.SetActive(true);
    }

    private void Hide()
    {
        uiParent.SetActive(false);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
        }
    }
}
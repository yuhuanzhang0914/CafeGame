using System;
using UnityEngine;
using TMPro;

public class CountDownUI : MonoBehaviour
{
    private const string IS_SHAKE = "IsShake";

    [SerializeField] private TextMeshProUGUI numberText;

    private Animator anim;
    private int preNumber = -1;

    private void Start()
    {
        anim = GetComponent<Animator>();

        // 订阅 GameManager 状态变化事件
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        // 初始化一次显示
        UpdateVisual();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
        }
    }

    private void Update()
    {
        // 只有在倒计时状态下才更新数字和播放动画
        if (GameManager.Instance.IsCountDownState())
        {
            // 从 GameManager 里拿当前倒计时（注意这里用的是你 GameManager 里的 GetCountDownToStartTimer）
            int nowNumber = Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer());

            // 只有数字变化时才更新文字和触发动画，避免每帧都触发
            if (nowNumber != preNumber)
            {
                preNumber = nowNumber;
                numberText.text = nowNumber.ToString();
                anim.SetTrigger(IS_SHAKE);
                SoundManager.Instance.PlayCountDownSound();
            }
        }
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (GameManager.Instance.IsCountDownState())
        {
            numberText.gameObject.SetActive(true);

            // 同时更新一次文本，保证刚进入倒计时时就有正确的数字
            int nowNumber = Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer());
            preNumber = nowNumber;
            numberText.text = nowNumber.ToString();
        }
        else
        {
            numberText.gameObject.SetActive(false);
        }
    }
}
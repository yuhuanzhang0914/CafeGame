using System;
using UnityEngine;
using TMPro;  

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;  

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        UpdateVisual(); 
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }
    private void Update()
    {
        if(GameManager.Instance.IsCountDownState())
        {
            numberText.text = Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer()).ToString();
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
        }
        else
        {
           numberText.gameObject.SetActive(false);
        }
    }
}
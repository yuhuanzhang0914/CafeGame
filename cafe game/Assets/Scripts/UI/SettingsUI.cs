using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get; private set; }
    [SerializeField] private GameObject uiParent;
    [SerializeField] private Button soundButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;
    [SerializeField] private Button musicButton;
   [SerializeField] private TextMeshProUGUI musicButtonText;
    [SerializeField] private Button closeButton;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Hide();
        UpdateVisual();

        soundButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
           
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    public  void Show()
    {
        uiParent.SetActive(true);
    }

    private void Hide()
    {
        uiParent.SetActive(false);
    }
    void UpdateVisual()
    {
        soundButtonText.text = "Sound volume:" + SoundManager.Instance.GetVolume();
        musicButtonText.text = "Music volume:" + MusicManager.Instance.GetVolume();
    }
}
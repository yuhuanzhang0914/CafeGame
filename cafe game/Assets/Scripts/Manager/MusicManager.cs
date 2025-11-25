using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float originalVolume;

    // 用户可以设置的音量档位 0~10（整数）
    private int volume = 5;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalVolume = audioSource.volume;
    }

    public void ChangeVolume()
    {
        volume++;
        if (volume > 10)
        {
            volume = 0;
        }

        audioSource.volume = originalVolume * (volume/10.0f);
    }

    public int GetVolume()
    {
        return volume;
    }
}
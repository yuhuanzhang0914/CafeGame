using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsS0 audioClipRefsSO;

    // 整数音量 0-10
    private int volume = 5;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OrderManager.Instance.OnRecipeSuccess += OrderManager_OnRecipeSuccess;
        OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
        CuttingCounter.OnCut += CuttingCounter_OnCut;
        KitchenObjectHolder.OnDrop += KitchenObjectHolder_OnDrop;
        KitchenObjectHolder.OnPickup += KitchenObjectHolder_OnPickup;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
    }

    private void TrashCounter_OnObjectTrashed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.trash);
    }

    private void KitchenObjectHolder_OnPickup(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup);
    }

    private void KitchenObjectHolder_OnDrop(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectDrop);
    }

    private void CuttingCounter_OnCut(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.chop);
    }

    private void OrderManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess, Camera.main.transform.position);
    }

    private void OrderManager_OnRecipeFailed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail, Camera.main.transform.position);
    }

    // 👉 倒计时音效：改成 public，外部可以调用
    public void PlayCountDownSound(float volumeMultiplier = 1f)
    {
        // 使用带 Camera.main 的那个重载
        PlaySound(audioClipRefsSO.warning, volumeMultiplier);
    }

    public void PlayStepSound(float volumeMultiplier = 1f)
    {
        PlaySound(audioClipRefsSO.footstep, volumeMultiplier);
    }

    private void PlaySound(AudioClip[] clips, Vector3 position, float volumeMultiplier = 1f)
    {
        if (clips == null || clips.Length == 0) return;

        int index = UnityEngine.Random.Range(0, clips.Length);
        AudioClip clip = clips[index];

        AudioSource.PlayClipAtPoint(clip, position, volume * 0.1f * volumeMultiplier);
    }

    private void PlaySound(AudioClip[] clips, float volumeMultiplier = 1f)
    {
        if (Camera.main == null) return;
        PlaySound(clips, Camera.main.transform.position, volumeMultiplier);
    }

    public void ChangeVolume()
    {
        volume++;
        if (volume > 10)
        {
            volume = 0;
        }
    }

    public int GetVolume()
    {
        return volume;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsS0 audioClipRefsSO;

    private void Start()
    {
        OrderManager.Instance.OnRecipeSuccess += OrderManager_OnRecipeSuccess;
        OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
        CuttingCounter.OnCut += CuttingCounter_OnCut;
        KitchenObjectHolder.OnDrop += KitchenObjectHolder_OnDrop;
        KitchenObjectHolder.OnPickup += KitchenObjectHolder_OnPickup;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
    }
    private void TrashCounter_OnObjectTrashed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.trash);
    }


    private void KitchenObjectHolder_OnPickup(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup);
    }

    private void KitchenObjectHolder_OnDrop(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectDrop);
    }




    private void CuttingCounter_OnCut(object sender,System. EventArgs e)
    {
        PlaySound(audioClipRefsSO.chop);
    }

    private void OrderManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        // 成功时播放成功音效
        PlaySound(audioClipRefsSO.deliverySuccess, Camera.main.transform.position);
    }

    private void OrderManager_OnRecipeFailed(object sender, EventArgs e)
    {
        // 失败时播放失败音效
        PlaySound(audioClipRefsSO.deliveryFail, Camera.main.transform.position);
    }

    // 只传数组的简写，在主摄像机位置播放
    private void PlaySound(AudioClip[] clips, float volume = 1.0f)
    {
        PlaySound(clips, Camera.main.transform.position, volume);
    }

    // 真正播放函数
    private void PlaySound(AudioClip[] clips, Vector3 position, float volume = 1.0f)
    {
        int index = UnityEngine.Random.Range(0, clips.Length);
        AudioSource.PlayClipAtPoint(clips[index], position, volume);
    }
}
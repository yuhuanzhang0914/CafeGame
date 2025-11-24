using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dotText;

    // 每次增加一个点的时间间隔
    private float dotRate = 0.3f;

    private void Start()
    {
        // 开始播放小点点动画
        StartCoroutine(DotAnimation());
    }

    private IEnumerator DotAnimation()
    {
        while (true)
        {
            dotText.text = ".";
            yield return new WaitForSeconds(dotRate);

            dotText.text = "..";
            yield return new WaitForSeconds(dotRate);

            dotText.text = "...";
            yield return new WaitForSeconds(dotRate);

            dotText.text = "....";
            yield return new WaitForSeconds(dotRate);

            dotText.text = ".....";
            yield return new WaitForSeconds(dotRate);

            dotText.text = "......";
            yield return new WaitForSeconds(dotRate);
        }
    }
}
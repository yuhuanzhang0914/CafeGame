using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnvisual;
    [SerializeField] private GameObject sizzlingParticles;

    public void ShowStoveEffect()
    {
        stoveOnvisual.SetActive(true);
        sizzlingParticles.SetActive(true);
    }
    public void HideStoveEffect()
    {
        stoveOnvisual.SetActive(false);
        sizzlingParticles.SetActive(false);
    }
}

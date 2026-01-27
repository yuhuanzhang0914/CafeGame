using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterText : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    public AudioSource typingSound;

    private TextMeshProUGUI textComponent;
    private string fullText;
    private Coroutine typingCoroutine;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        fullText = textComponent.text;
        textComponent.text = "";
    }

    void OnEnable()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        textComponent.text = "";
        typingCoroutine = StartCoroutine(TypeText());
    }

    void OnDisable()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        if (typingSound != null && typingSound.isPlaying)
            typingSound.Stop();
    }

    IEnumerator TypeText()
    {
        if (typingSound != null)
            typingSound.Play();

        foreach (char letter in fullText)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (typingSound != null)
            typingSound.Stop();
    }
}
































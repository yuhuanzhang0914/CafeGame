using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [Header("Ending Panels")]
    public GameObject endingGood;
    public GameObject endingBad;

    [Header("Next Button")]
    public Button nextButton; 

    [HideInInspector] public bool didPlayerDoWell;

    void Awake()
    {
        didPlayerDoWell = GameManager.didPlayerDoWellStatic;
    }

    void Start()
    {
       
        if (endingGood != null) endingGood.SetActive(false);
        if (endingBad != null) endingBad.SetActive(false);

        ShowEnding();

       
        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextClicked);
    }

    public void ShowEnding()
    {
        if (didPlayerDoWell && endingGood != null)
            endingGood.SetActive(true);
        else if (!didPlayerDoWell && endingBad != null)
            endingBad.SetActive(true);
        else
            Debug.LogError("EndingManager: Missing ending panel!");
    }

    private void OnNextClicked()
    {
       
        SceneManager.LoadScene("1-GameMenu");
    }
}


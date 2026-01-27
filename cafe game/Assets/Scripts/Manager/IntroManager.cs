using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject[] panels;

    [Header("Next Button")]
    public Button nextButton; 

    [Header("Scene To Load After Intro")]
    public string nextSceneName = "1-GameMenu";

    private int currentIndex = 0;

    void Start()
    {
       
        if (panels == null || panels.Length == 0)
        {
            Debug.LogError("IntroManager: No panels assigned!");
            return;
        }

       
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }

        
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(NextPanel);
        }
        else
        {
            Debug.LogError("IntroManager: Next Button not assigned!");
        }
    }

    public void NextPanel()
    {
       
        if (currentIndex >= panels.Length)
            return;

        
        panels[currentIndex].SetActive(false);
        currentIndex++;

        if (currentIndex < panels.Length)
        {
            
            panels[currentIndex].SetActive(true);
        }
        else
        {
            
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [Header("Panels")]
    public gameObject[] panels; // Assign your 6 panels in order in the Inspector

    [Header("Next Button")]
    public Button nextButton; // Assign your Next button in the Inspector

    [Header("Main Menu")]
    public gameObject GameMenu; // Assign your GameMenu UI here

    private int currentIndex = 0;

    void Start()
    {
        // Show only the first panel
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(i == 0);

        // Hook up the Next button to call NextPanel
        nextButton.onClick.AddListener(NextPanel);

        // Hide the main menu at start
        if (gameMenu != null)
            gameMenu.SetActive(false);
    }

    public void NextPanel()
    {
        // Hide current panel
        panels[currentIndex].SetActive(false);

        // Move to next panel
        currentIndex++;

        if (currentIndex < panels.Length)
        {
            // Show next panel
            panels[currentIndex].SetActive(true);
        }
        else
        {
            // Finished all panels → show main menu
            if (gameMenu != null)
                gameMenu.SetActive(true);

            // Disable intro panels and button
            gameObject.SetActive(false);
            nextButton.interactable = false;
        }
    }
}

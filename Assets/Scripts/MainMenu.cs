using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Button startGameButton;
    private Button quitGameButton;
    private Button creditsButton;
    private Button creditsBackButton;
    private GameObject creditsPanel;

    private bool creditsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        startGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
        quitGameButton = GameObject.Find("QuitGameButton").GetComponent<Button>();
        creditsButton = GameObject.Find("CreditsButton").GetComponent<Button>();
        creditsBackButton = GameObject.Find("CreditsBackButton").GetComponent<Button>();
        creditsPanel = GameObject.Find("CreditsPanel");

        startGameButton.onClick.AddListener(StartGame);
        quitGameButton.onClick.AddListener(QuitGame);
        creditsButton.onClick.AddListener(ToggleCredits);
        creditsBackButton.onClick.AddListener(ToggleCredits);

        creditsPanel.SetActive(false);
    }

    void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void ToggleCredits()
    {
        creditsActive = !creditsActive;
        creditsPanel.SetActive(creditsActive);
    }
}

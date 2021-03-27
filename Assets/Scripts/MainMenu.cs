using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Button startGameButton;
    private Button quitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        startGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
        quitGameButton = GameObject.Find("QuitGameButton").GetComponent<Button>();

        startGameButton.onClick.AddListener(StartGame);
        quitGameButton.onClick.AddListener(QuitGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}

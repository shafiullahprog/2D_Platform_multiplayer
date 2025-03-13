using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameOverScreen;

    void Start()
    {
        ShowStartScreen();
    }

    public void ShowStartScreen()
    {
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

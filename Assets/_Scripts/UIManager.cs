using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]  GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] Button gameOver;
    
    public static UnityAction OnGameOver;
    private void Start()
    {
        gameOver.onClick.AddListener(QuitGame);
    }
    public void WinScreen()
    {
        gameOver.gameObject.SetActive(true);
        winPanel.SetActive(true);
        OnGameOver?.Invoke();
    }

    public void LoseScreen()
    {
        gameOver.gameObject.SetActive(true);
        losePanel.SetActive(true);
        OnGameOver?.Invoke();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

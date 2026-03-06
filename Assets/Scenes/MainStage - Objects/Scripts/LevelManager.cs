using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
            Application.Quit();   
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(2);
    }

}

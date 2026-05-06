using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public bool IsGamePaused = false;

    public GameObject pauseMenuUI;

    public GameObject ScoreManager;

    public InputActionReference pauseAction;

    private ScoreManager scoreManagerScript;

    void Start()
    {
        if (ScoreManager == null) return;
            
        scoreManagerScript = ScoreManager.GetComponent<ScoreManager>();

        pauseMenuUI.SetActive(false);
      
        pauseAction.action.Enable();

    }

    // Update is called once per frame
    void Update()
    {
    
        if (pauseAction.action.WasPressedThisFrame())
        {

            if(!IsGamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
       
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

}

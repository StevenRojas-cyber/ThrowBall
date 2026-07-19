using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; 

public class TutorialManager : MonoBehaviour
{
    public InputActionReference TutorialAction;

    public GameObject TutorialUI;

    public GameObject Pause;

    void Start()
    {
        if (TutorialUI == null) return;

        if(TutorialAction == null) return;

        //if (Pause == null) return;


        TutorialUI.SetActive(true);

        //Pause.GetComponent<PauseMenu>().pauseAction.action.Disable();

        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if(TutorialAction.action.WasPressedThisFrame())
        {
            print("Quitar pausa");

            Time.timeScale = 1f;
            
            TutorialUI.SetActive(false);

            //Pause.GetComponent<PauseMenu>().pauseAction.action.Enable();
        }
    }
}

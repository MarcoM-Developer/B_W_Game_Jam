using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : GameState
{
    private GameObject pauseMenu;

    public delegate void Pause();

    public static event Pause OnPause;

    public delegate void Resume();

    public static event Resume OnResume;

    public override void StartState()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");

        Debug.Log("Starting pause State");

        if (OnPause != null)
        {
            OnPause();
        }
        pauseMenu.transform.GetChild(0).gameObject.SetActive(true);
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndingState();
        }
    }

    public override void EndingState()
    {
        if(OnResume != null)
        {
            OnResume();
        }

        pauseMenu.transform.GetChild(0).gameObject.SetActive(false);
        GameStateManager.CurrentGameState = GameStateManager.GamePlayState;
        Debug.Log("Ending Pause State");
        GameStateManager.CurrentGameState.StartState();
    }
}

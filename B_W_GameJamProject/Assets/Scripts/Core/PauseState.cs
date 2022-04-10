using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseState : GameState
{
    private GameObject pauseMenu;

    public static event Action OnPause;

    public static event Action OnResume;

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

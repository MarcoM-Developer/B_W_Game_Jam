using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : GameState
{

    [SerializeField] private int frameRate;

    public override void StartState()
    {
        //Application.targetFrameRate = frameRate;
        //QualitySettings.vSyncCount = 1;
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndingState();
            GameStateManager.CurrentGameState = GameStateManager.PauseState;
            GameStateManager.CurrentGameState.StartState();
        }
    }

    public override void EndingState()
    {
        Debug.Log("Ending GameplayState");
    }
}

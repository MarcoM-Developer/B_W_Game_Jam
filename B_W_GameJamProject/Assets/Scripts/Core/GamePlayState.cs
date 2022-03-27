using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : GameState
{

    public override void StartState()
    {

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

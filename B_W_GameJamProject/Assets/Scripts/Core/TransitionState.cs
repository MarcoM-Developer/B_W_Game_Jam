using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TransitionState : GameState
{

    public static Action OnTransition;
    public static Action OnEndingTransition;

    public override void StartState()
    {
        if (OnTransition!=null)
        {
            OnTransition();
        }
    }

    public override void UpdateState()
    {
        
    }

    public override void EndingState()
    {
        if (OnEndingTransition != null)
        {
            OnEndingTransition();
        }
        GameStateManager.CurrentGameState = GameStateManager.GamePlayState;
        GameStateManager.CurrentGameState.StartState();
    }
}

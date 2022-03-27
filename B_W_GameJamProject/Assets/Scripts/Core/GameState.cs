using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;

    public GameStateManager GameStateManager { get => gameStateManager; set => gameStateManager = value; }

    public abstract void StartState();

    public abstract void UpdateState();

    public abstract void EndingState();
}

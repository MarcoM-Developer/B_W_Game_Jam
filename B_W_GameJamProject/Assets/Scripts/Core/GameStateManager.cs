using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private GameState currentGameState;
    [SerializeField] private PauseState pauseState;
    [SerializeField] private GamePlayState gamePlayState;
    [SerializeField] private TransitionState transitionState;

    public GameState CurrentGameState { get => currentGameState; set => currentGameState = value; }
    public PauseState PauseState { get => pauseState; set => pauseState = value; }
    public GamePlayState GamePlayState { get => gamePlayState; set => gamePlayState = value; }
    public TransitionState TransitionState { get => transitionState; set => transitionState = value; }

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = transitionState;
        currentGameState.StartState();
    }

    // Update is called once per frame
    void Update()
    {
        currentGameState.UpdateState();
    }
}

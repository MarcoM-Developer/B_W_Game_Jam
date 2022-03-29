using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{

    [SerializeField] private GroundedState groundedState;
    [SerializeField] private JumpingState jumpingState;
    
    private PlayerState currentState;

    public PlayerState CurrentState { get => currentState; set => currentState = value; }
    public GroundedState GroundedState { get => groundedState; set => groundedState = value; }
    public JumpingState JumpingState { get => jumpingState; set => jumpingState = value; }


    // Start is called before the first frame update
    void Start()
    {
        currentState = groundedState;
        currentState.StartState();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }
}

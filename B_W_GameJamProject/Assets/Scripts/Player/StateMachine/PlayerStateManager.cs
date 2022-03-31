using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private OnState onState;
    [SerializeField] private OffState offState;
    [SerializeField] private GroundedState groundedState;
    [SerializeField] private JumpingState jumpingState;
    
    private PlayerState currentState;

    public PlayerState CurrentState { get => currentState; set => currentState = value; }
    public GroundedState GroundedState { get => groundedState; set => groundedState = value; }
    public JumpingState JumpingState { get => jumpingState; set => jumpingState = value; }
    public OnState OnState { get => onState; set => onState = value; }
    public OffState OffState { get => offState; set => offState = value; }

    private void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    private void OnDisable()
    {
       
    }
}

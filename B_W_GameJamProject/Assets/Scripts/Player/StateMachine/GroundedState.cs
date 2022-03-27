using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerState
{
    [SerializeField] private Movement playerMovement;

    public Movement PlayerMovement { get => playerMovement; set => playerMovement = value; }

    public override void StartState()
    {
        PauseState.OnPause += DisablePlayerMovement;
        PauseState.OnResume += EnablePlayerMovement;
        EnablePlayerMovement();
    }

    public override void UpdateState()
    {
        
    }

    public override void EndingState()
    {
        
    }

    private void OnDisable()
    {
        PauseState.OnPause += DisablePlayerMovement;
        PauseState.OnResume += EnablePlayerMovement;
    }

    private void DisablePlayerMovement()
    {
        playerMovement.enabled = false;
    }
    
    private void EnablePlayerMovement()
    {
        playerMovement.enabled = true;
    }


}

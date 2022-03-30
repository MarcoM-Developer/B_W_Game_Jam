using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerState
{
    [SerializeField] private Movement playerMovement;
    [SerializeField] private Jump playerJump;

    public Movement PlayerMovement { get => playerMovement; set => playerMovement = value; }

    public override void StartState()
    {
        PauseState.OnPause += DisablePlayerMovement;
        PauseState.OnPause += DisablePlayerJump;

        PauseState.OnResume += EnablePlayerMovement;
        PauseState.OnResume += EnablePlayerJump;


        EnablePlayerMovement();
        EnablePlayerJump();
    }

    public override void UpdateState()
    {
        
    }

    public override void EndingState()
    {

    }

    private void OnDisable()
    {
        PauseState.OnPause -= DisablePlayerMovement;
        PauseState.OnPause -= DisablePlayerJump;

        PauseState.OnResume -= EnablePlayerMovement;
        PauseState.OnResume -= EnablePlayerJump;

    }

    private void DisablePlayerMovement()
    {
        playerMovement.enabled = false;
    }

    private void EnablePlayerMovement()
    {
        playerMovement.enabled = true;
    }

    private void DisablePlayerJump()
    {
        playerJump.enabled = false;
    }

    private void EnablePlayerJump()
    {
        playerJump.enabled = true;
        playerJump.CanJump = true;
    }

}

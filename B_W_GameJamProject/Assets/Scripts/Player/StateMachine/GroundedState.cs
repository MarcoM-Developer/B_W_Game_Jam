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
        EnablePlayerMovement();
        EnablePlayerJump();
    }

    public override void UpdateState()
    {
        
    }

    public override void EndingState()
    {

    }


    private void EnablePlayerMovement()
    {
        playerMovement.enabled = true;
    }


    private void EnablePlayerJump()
    {
        playerJump.enabled = true;
        playerJump.CanJump = true;
    }

}

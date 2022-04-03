using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : PlayerState
{

    [SerializeField] private Movement playerMovement;
    [SerializeField] private Jump playerJump;

    public override void StartState()
    {
        playerMovement.enabled = true;

        if (playerJump != null)
        {
            Debug.Log("Player does not jump here");
            playerJump.CanJump = false;
        }

    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("I'm Already Jumping");
        }
    }

    public override void EndingState()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private PlayerStateManager playerStateManager;


    public bool IsActive { get => isActive; set => isActive = value; }

    private void OnEnable()
    {
       
    }

    // Start is called before the first frame update
    private void Start()
    {
        //CheckIfPlayerIsActive();
        CheckIsActiveThenActivatePlayer();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isActive = !isActive;
            CheckIsActiveThenActivatePlayer();
        }
    }

    private void CheckIsActiveThenActivatePlayer()
    {
        if (isActive)
        {
            playerStateManager.CurrentState = playerStateManager.OnState;
            playerStateManager.CurrentState.StartState();
        }
        else
        {
            playerStateManager.CurrentState = playerStateManager.OffState;
            playerStateManager.CurrentState.StartState();
        }
    }

    /*private void SwitchPlayerControlOverScripts()
    {
        foreach (Behaviour behaviour in scriptsList)
        {
            if (IsActive)
            {
                behaviour.enabled = true;
                behaviour.enabled = false;
            }
            else
            {
                behaviour.enabled = false;
            }
        }
    }

    private void CheckIfPlayerIsActive()
    {
        foreach (Behaviour behaviour in scriptsList)
        {
            if (IsActive)
            {
                behaviour.enabled = true;
            }
            else
            {
                behaviour.enabled = false;
            }
        }
    }*/
}

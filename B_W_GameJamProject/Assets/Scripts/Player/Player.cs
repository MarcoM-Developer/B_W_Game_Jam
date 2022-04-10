using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private PlayerType playerType;
    [SerializeField] private Vector3Variable playerPosition;
    public static event Action OnSwitchCharacter;


    public bool IsActive { get => isActive; set => isActive = value; }
    public PlayerType PlayerType { get => playerType; set => playerType = value; }

    private void OnEnable()
    {
        TransitionState.OnTransition += DisablePlayer;
        TransitionState.OnEndingTransition += CheckIsActiveThenPassState;
        SaveManager.OnSave += StorePlayerPosition;
        SaveManager.OnLoad += LoadPlayerPosition;
    }

    // Start is called before the first frame update
    private void Start()
    {
        CheckIsActiveThenPassState();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCharacter();
            if (OnSwitchCharacter != null)
            {
                OnSwitchCharacter();
            }
        }
    }

    private void OnDisable()
    {
        TransitionState.OnTransition -= DisablePlayer;
        TransitionState.OnEndingTransition -= CheckIsActiveThenPassState;
        SaveManager.OnSave -= StorePlayerPosition;
        SaveManager.OnLoad -= LoadPlayerPosition;
    }

    private void CheckIsActiveThenPassState()
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

    private void SwitchCharacter()
    {
        isActive = !isActive;
        CheckIsActiveThenPassState();
    }

    private void StorePlayerPosition()
    {
        playerPosition.Value = transform.position;
    }

    private void LoadPlayerPosition()
    {
        transform.position = playerPosition.Value;
    }

    private void DisablePlayer()
    {
        playerStateManager.CurrentState = playerStateManager.OffState;
        playerStateManager.CurrentState.StartState();
    }
}

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
        //Player Movement
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Play player movement at current player
            SwitchCharacter();
            if (OnSwitchCharacter != null)
            {
                OnSwitchCharacter();
            }
        }
    }

    private void OnDisable()
    {
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
}

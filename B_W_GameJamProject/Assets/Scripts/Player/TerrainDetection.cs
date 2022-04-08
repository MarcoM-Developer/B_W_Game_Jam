using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetection : MonoBehaviour
{
    [SerializeField] private FloatReference detectionRange;
    public LayerMask layerTarget;
    [SerializeField] private Player player;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private float groundedTimer;
    [SerializeField] private float groundedTimerValue;
    private bool flag;

    //For Audio
    public bool playerLanded = false;

    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        CheckPlayerTypeForCollisionTargets();

    }

    // Update is called once per frame
    void Update()
    {
        groundedTimerValue -= Time.deltaTime;

        Collider2D groundCollider = Physics2D.OverlapCircle(transform.position, transform.parent.transform.lossyScale.x / detectionRange.Value, layerTarget);
        //Debug.Log(groundCollider);
        if (groundCollider != null)
        {
            if (playerStateManager.CurrentState is InAirState)
            {
                playerStateManager.CurrentState.EndingState();
                playerStateManager.CurrentState = playerStateManager.GroundedState;
                playerStateManager.CurrentState.StartState();
                playerLanded = true;// to tell audio script
            }
            else if (playerStateManager.CurrentState is OnState)
            {
                playerStateManager.CurrentState.EndingState();
                playerStateManager.CurrentState = playerStateManager.GroundedState;
                playerStateManager.CurrentState.StartState();
                
            }

        }
        else if (groundCollider == null)
        {
            if (playerStateManager.CurrentState is GroundedState)
            {
                GoToJumpingstateAfterBriefTimeInterval();
                playerLanded = false; // to tell audio script
            }
            else if (playerStateManager.CurrentState is OnState)
            {
                playerStateManager.CurrentState.EndingState();
                playerStateManager.CurrentState = playerStateManager.InAirState;
                playerStateManager.CurrentState.StartState();
            }
        }
    }

    private void GoToJumpingstateAfterBriefTimeInterval()
    {
        if (!flag)
        {
            groundedTimerValue = groundedTimer;
            flag = true;
        }
        if (groundedTimerValue <= 0)
        {
            playerStateManager.CurrentState.EndingState();
            playerStateManager.CurrentState = playerStateManager.InAirState;
            playerStateManager.CurrentState.StartState();
            flag = false;
        }
    }

    private void CheckPlayerTypeForCollisionTargets()
    {
        switch (player.PlayerType)
        {
            case PlayerType.White:
                layerTarget = LayerMask.GetMask("WhiteWalls");
                break;

            case PlayerType.Black:
                layerTarget = LayerMask.GetMask("BlackWalls");
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.parent.transform.lossyScale.x / detectionRange.Value);
    }
}
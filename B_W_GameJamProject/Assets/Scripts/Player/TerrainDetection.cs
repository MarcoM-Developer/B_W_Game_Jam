using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetection : MonoBehaviour
{
    [SerializeField] private FloatReference detectionRange;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private float groundedTimer;
    [SerializeField] private float groundedTimerValue;
    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        groundedTimerValue -= Time.deltaTime;

        Collider2D groundCollider = Physics2D.OverlapCircle(transform.position, detectionRange.Value, layerTarget);
        //Debug.Log(groundCollider);
        if(groundCollider != null)
        {
            if (playerStateManager.CurrentState is InAirState)
            {
                playerStateManager.CurrentState.EndingState();
                playerStateManager.CurrentState = playerStateManager.GroundedState;
                playerStateManager.CurrentState.StartState();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange.Value);
    }
}

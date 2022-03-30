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
    private bool isInAir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        groundedTimerValue -= Time.deltaTime;

        Collider2D groundCollider = Physics2D.OverlapCircle(transform.position, detectionRange.Value, layerTarget);

        if(groundCollider != null)
        {
            if (playerStateManager.CurrentState is JumpingState)
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
                if (!isInAir)
                {
                    groundedTimerValue = groundedTimer;
                    isInAir = true;
                }
                if (groundedTimerValue <= 0)
                {
                    playerStateManager.CurrentState.EndingState();
                    playerStateManager.CurrentState = playerStateManager.JumpingState;
                    playerStateManager.CurrentState.StartState();
                    isInAir = false;
                }
            }
            else if (playerStateManager.CurrentState is OnState)
            {
                playerStateManager.CurrentState.EndingState();
                playerStateManager.CurrentState = playerStateManager.JumpingState;
                playerStateManager.CurrentState.StartState();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange.Value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetection : MonoBehaviour
{
    [SerializeField] private FloatReference range;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private PlayerStateManager playerStateManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        Collider2D collider = Physics2D.OverlapCircle(transform.position, range.Value, layerTarget);

        Debug.Log(collider); 

        if(collider != null)
        {
            if (playerStateManager.CurrentState is JumpingState)
            {
                playerStateManager.CurrentState.EndingState();
                playerStateManager.CurrentState = playerStateManager.GroundedState;
                playerStateManager.CurrentState.StartState();
            }
            
        }
        else if (collider == null)
        {
            if (playerStateManager.CurrentState is GroundedState)
            {
                playerStateManager.CurrentState.EndingState();
                playerStateManager.CurrentState = playerStateManager.JumpingState;
                playerStateManager.CurrentState.StartState();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range.Value);
    }
}

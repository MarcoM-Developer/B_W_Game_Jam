using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffState : PlayerState , IScriptComponentsHandler
{
    [SerializeField] private List<Behaviour> scripts;
    private Rigidbody2D playerRigidBody;
    public override void StartState()
    {
        ChangeScriptStatus(scripts, false);
        playerRigidBody = GetComponentInParent<Rigidbody2D>();
        playerRigidBody.velocity = new Vector2(0 , playerRigidBody.velocity.y);
    }

    public override void UpdateState()
    {
        playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
    }

    public override void EndingState()
    {

    }

    public void ChangeScriptStatus(List<Behaviour> scripts, bool isActive)
    {
        foreach (Behaviour script in scripts)
        {
            if (script != null)
            {
                script.enabled = isActive;
            }
        }
    }
}

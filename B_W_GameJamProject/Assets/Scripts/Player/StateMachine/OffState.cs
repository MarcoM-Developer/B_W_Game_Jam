using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffState : PlayerState , IScriptComponentsHandler
{
    [SerializeField] private List<Behaviour> scripts;

    public override void StartState()
    {
        ChangeScriptStatus(scripts, false);
        Rigidbody2D playerRigidBody = GetComponentInParent<Rigidbody2D>();
        playerRigidBody.velocity = new Vector2(0 , playerRigidBody.velocity.y);
    }

    public override void UpdateState()
    {
        
    }

    public override void EndingState()
    {

    }

    public void ChangeScriptStatus(List<Behaviour> scripts, bool isActive)
    {
        foreach (Behaviour script in scripts)
        {
            script.enabled = isActive;
        }
    }
}

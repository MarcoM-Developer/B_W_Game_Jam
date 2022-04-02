using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnState : PlayerState, IScriptComponentsHandler
{
    [SerializeField] private List<Behaviour> scripts;

    public override void StartState()
    {
        ChangeScriptStatus(scripts,true);
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

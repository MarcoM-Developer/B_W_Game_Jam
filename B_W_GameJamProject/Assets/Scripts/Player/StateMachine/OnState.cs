using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnState : PlayerState, IScriptComponentsHandler
{
    [SerializeField] private List<Behaviour> scripts;

    public override void StartState()
    {
        //EnableScripts();
        ChangeScriptStatus(scripts,true);
    }

    public override void UpdateState()
    {
        
    }

    public override void EndingState()
    {
       
    }

    private void EnableScripts()
    {
        foreach (Behaviour script in scripts)
        {
            script.enabled = true;
        }
    }

    public List<Behaviour> ChangeScriptStatus(List<Behaviour> scripts, bool isActive)
    {
        foreach (Behaviour script in scripts)
        {
            script.enabled = isActive;
        }
        return scripts;
    }
}

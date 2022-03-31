using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffState : PlayerState , IScriptComponentsHandler
{
    [SerializeField] private List<Behaviour> scripts;

    public override void StartState()
    {
        // DisableScripts();
        scripts = ChangeScriptStatus(scripts, false);
    }

    public override void UpdateState()
    {
        
    }

    public override void EndingState()
    {

    }

    private void DisableScripts()
    {
        foreach (Behaviour script in scripts)
        {
            script.enabled = false;
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

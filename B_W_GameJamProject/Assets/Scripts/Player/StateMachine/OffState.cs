using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffState : PlayerState
{
    [SerializeField] private List<Behaviour> scripts;

    public override void StartState()
    {
        foreach(Behaviour script in scripts)
        {
            script.enabled = false;
        }
    }

    public override void UpdateState()
    {
        
    }

    public override void EndingState()
    {

    }
}

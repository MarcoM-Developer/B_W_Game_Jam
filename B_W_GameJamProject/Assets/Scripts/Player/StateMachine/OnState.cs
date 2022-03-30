using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnState : PlayerState
{
    [SerializeField] private TerrainDetection terrainDetection;

    public override void StartState()
    {
        terrainDetection.enabled = true;
    }

    public override void UpdateState()
    {
        
    }

    public override void EndingState()
    {
       
    }
}

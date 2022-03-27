using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour   //Monobehaviour inheritance just to attach states subclasses to gameobjects
{
    public abstract void StartState();
    public abstract void UpdateState();
    public abstract void EndingState();
}

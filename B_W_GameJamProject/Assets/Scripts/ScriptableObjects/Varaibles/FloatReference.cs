using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FloatReference
{
    [SerializeField] private bool useLocalValue = true;
    [SerializeField] private float localValue;
    [SerializeField] private FloatVariable variable;

    public float Value
    {
        get { return useLocalValue ? localValue : variable.Value; }
        
        set 
        {
            {
                if (useLocalValue)
                {
                    localValue = value;
                }
                else
                {
                    variable.Value = value;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BoolReference 
{
    [SerializeField] private bool useLocalValue = true;
    [SerializeField] private bool localValue;
    [SerializeField] private BoolVariable variable;

    public bool Value
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

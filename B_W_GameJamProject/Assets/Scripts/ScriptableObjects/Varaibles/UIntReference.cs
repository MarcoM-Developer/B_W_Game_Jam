using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UIntReference
{
    [SerializeField] private bool useLocalValue = true;
    [SerializeField] private uint localValue;
    [SerializeField] private UIntVariable variable;

    public uint Value
    {
        get
        {
            return useLocalValue ? localValue : variable.Value;
        }

        set
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

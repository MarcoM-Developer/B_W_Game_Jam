using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringReference : MonoBehaviour
{
    [SerializeField] private bool useLocalValue = true;
    [SerializeField] private string localValue;
    [SerializeField] private StringVariable variable;

    public string Value
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

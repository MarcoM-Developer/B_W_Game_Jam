using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Variables/BoolVariable")]
public class BoolVariable : ScriptableObject
{
    [SerializeField] private bool value;

    public bool Value { get => value; set => this.value = value; }
}

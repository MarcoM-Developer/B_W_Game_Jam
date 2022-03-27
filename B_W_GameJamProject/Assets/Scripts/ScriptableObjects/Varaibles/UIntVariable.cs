using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Variables/UIntVariable")]
public class UIntVariable : ScriptableObject
{
    [SerializeField] private uint value;

    public uint Value { get => value; set => this.value = value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Variables/Vector3Variable")]
public class Vector3Variable : ScriptableObject
{
    [SerializeField] private Vector3 value;

    public Vector3 Value { get => value; set => this.value = value; }
}

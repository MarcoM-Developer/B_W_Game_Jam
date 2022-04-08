using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public const string BLACKWIRE = "blackWire";
    public const string WHITEWIRE = "whiteWire";
    public const string CHECKERBOARD = "checkerboard";
    public const string TEMPWHITEWALL = "tempWhiteWall";
    public const string TEMPBLACKWALL = "tempBlackWall";

    public static readonly string[] TYPES = { BLACKWIRE, WHITEWIRE, CHECKERBOARD, TEMPBLACKWALL, TEMPWHITEWALL };

    public bool propagatesSignal = true; // Does the tile send signal?
    public string type = BLACKWIRE;

    public TileBase[] tiles;

    public bool isTemp, isWire, isCheckerboard, isSwitch;
    //public float spreadChance, spreadInterval, burnTime;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTile : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GridLayout gridLayout;
    [SerializeField] private BoundsInt area;
    [SerializeField] private List<TileBase> tiles;

    private void Start()
    {
        GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();

        //tiles.Add(tilemap.GetTile(new Vector3Int(12,-12,0)));

        foreach (TileBase tile in tilemap.GetTilesBlock(area))
        {
            tiles.Add(tile);
        }

        tilemap.SetTile(new Vector3Int(13,-12,0), null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(area.position,area.size);
    }
}

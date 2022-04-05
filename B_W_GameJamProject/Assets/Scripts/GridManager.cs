using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap blackTileMap; // Work with BlackMap for now

    [SerializeField]
    private Tilemap whiteTileMap;

    // Setup the dual map?
    void Start()
	{

	}


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
		{
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int blackTilePosition = blackTileMap.WorldToCell(mousePosition);
            Vector3Int whiteTilePosition = whiteTileMap.WorldToCell(mousePosition);

            TileBase blackTile = blackTileMap.GetTile(blackTilePosition);
            TileBase whiteTile = whiteTileMap.GetTile(whiteTilePosition);

            if (blackTile != null)
            {
                Debug.Log("Tile: " + blackTile.name);
            }

            if (whiteTile != null)
			{
                Debug.Log("Tile: " + whiteTile.name);
			}

            Spread(blackTilePosition);

        }
    }


    /**
     * Spreads the tiles to nearest neighbors.
     */
    public void Spread(Vector3Int position)
    {
        for (int x = position.x - 1; x < position.x + 2; x++)
        {
            for (int y = position.y - 1; y < position.y + 2; y++)
            {
                TryToSpreadTile(new Vector3Int(x, y, 0));
            }
        }

          
        void TryToSpreadTile(Vector3Int position)
        {
            TileBase tile = blackTileMap.GetTile(position);

            if (tile != null && tile.name=="Checkerboard")
            {
                blackTileMap.SetTile(position, null);
                Spread(position);
            }
        }
    }
}

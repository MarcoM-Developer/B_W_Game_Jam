using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map; // Work with BlackMap for now 

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
            Vector3Int position = map.WorldToCell(mousePosition);

            TileBase tile = map.GetTile(position);

            if (tile != null)
            {
                Debug.Log("Tile: " + tile.name);
            }

            Spread(position);

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
            TileBase tile = map.GetTile(position);

            if (tile != null && tile.name=="Checkerboard")
            {
                map.SetTile(position, null);
                Spread(position);
            }
        }
    }
}

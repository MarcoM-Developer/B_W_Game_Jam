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

    [SerializeField]
    private TileData blackWire;

    [SerializeField]
    private TileData whiteWire;

    [SerializeField]
    private TileData checkerboard;

    [SerializeField]
    private TileData tempWhiteWall;

    [SerializeField]
    private TileData tempBlackWall;

 
    // TODO: add explicit reference to white and black walls

    private Dictionary<TileBase, TileData> dataFromTiles;

    private List<Vector3Int> activeWires = new List<Vector3Int>();


    // Populate the dataFromTiles
    private void Awake()
	{
       
        dataFromTiles = new Dictionary<TileBase, TileData>();
        TileData[] tileData = { blackWire, whiteWire, checkerboard, tempWhiteWall, tempBlackWall };


        foreach(var data in tileData)
		{
            Debug.Log("Nonnull tile added");

            if (data.tile != null)
            {
               dataFromTiles.Add(data.tile, data);
            }
			
		}
	}



    // Setup the dual map?
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Test the activation

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int blackTilePosition = blackTileMap.WorldToCell(mousePosition);
            Vector3Int whiteTilePosition = whiteTileMap.WorldToCell(mousePosition);

            TileBase blackTile = blackTileMap.GetTile(blackTilePosition);
            TileBase whiteTile = whiteTileMap.GetTile(whiteTilePosition);

            

            if (blackTile != null)
            {
                
                Debug.Log("Black Tile: " + blackTile.name);

                if (dataFromTiles.ContainsKey(blackTile)) {
                    Debug.Log("Propagates?: " + dataFromTiles[blackTile].propagatesSignal);
                }
                Spread(blackTilePosition);
            }

            if (whiteTile != null)
            {
                Debug.Log("Black Tile: " + whiteTile.name);
            }
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

            if (tile != null)
            {
                switch (tile.name)
                {
                    case "Checkerboard":
                        blackTileMap.SetTile(position, null);
                        whiteTileMap.SetTile(position, tempWhiteWall.tile); // Do the positions match?

                        Spread(position);
                        break;

                    case "Wire":
                        if (!activeWires.Contains(position))
						{
                            activeWires.Add(position);
                            Spread(position);
                        }
                        
                        break;

                }
            }
        }
    }
}

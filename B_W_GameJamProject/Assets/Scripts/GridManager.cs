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

    [SerializeField]
    private TileData blackSwitch;

    [SerializeField]
    private TileData whiteSwitch;

    [SerializeField]
    private GameObject blackPlayer;

    [SerializeField]
    private GameObject whitePlayer;

    private Dictionary<TileBase, TileData> dataFromTiles;
    private List<Vector3Int> activeTiles = new List<Vector3Int>();


    /**
     * Populate the dataFromTiles, so I can fetch the tiles later
     */ 
    private void Awake()
	{
        dataFromTiles = new Dictionary<TileBase, TileData>();
        TileData[] tileData = { blackWire,
                                whiteWire,
                                checkerboard,
                                tempWhiteWall,
                                tempBlackWall,
                                whiteSwitch,
                                blackSwitch};

        foreach(var data in tileData)
        { 
            if (data.tiles != null)
            {
                foreach (var tile in data.tiles)
                {
                    dataFromTiles.Add(tile, data);
                }
            }
			
		}
	}



    // TODO: Setup the dual map?
    void Start()
    {

    }


    // HACK: This should be done using events, but who cares, it's a GameJam.
    private bool whiteOnSwitch = false; 
    private bool blackOnSwitch = false;


    // Update is called once per frame
    void Update()
    {
        // Black Player on White Tile
        Vector2 blackPlayerPosition = blackPlayer.transform.position;
        Vector3Int whiteTilePosition = whiteTileMap.WorldToCell(blackPlayerPosition);
        TileBase whiteTile = whiteTileMap.GetTile(whiteTilePosition);


        // White Player on Black Tile
        Vector2 whitePlayerPosition = whitePlayer.transform.position;
        Vector3Int blackTilePosition = whiteTileMap.WorldToCell(whitePlayerPosition);
        TileBase blackTile = blackTileMap.GetTile(blackTilePosition);

        // Check if White stands on switch.
        if (blackTile != null && dataFromTiles.ContainsKey(blackTile))
		{
            TileData data = dataFromTiles[blackTile];

            if (data.isSwitch)
			{
                // Yes, I stand on switch.

                if (!whiteOnSwitch)
				{
                    //... and in fact, I just moved to the switch...
                    Debug.Log("Entered switch.");

                    //... tell the world I am standing on the switch! 
                    whiteOnSwitch = true;

                    //... spread the word.
                    Spread(blackTilePosition);
                    PostSpread(blackTilePosition);
                }
			}else
			{
                // No, I do not stand on switch.

                if (whiteOnSwitch)
				{
                    //... and truth be told, I just left the switch.
                    Debug.Log("Exited switch.");

                    //... tell the world I shamelessly quit the switch!
                    whiteOnSwitch = false;

                    //... spread the word.
                    Spread(blackTilePosition);
                    PostSpread(blackTilePosition);

                }
			}
            //Debug.Log("Wire: "+data.isWire + "Switch: "+data.isSwitch);
		}

        // Mouse (for testing)
        if (Input.GetMouseButtonDown(0))
        {
            // Test the activation
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // HACK: WorldToCell gives consistent position on both grids, use just one
            Vector3Int position = blackTileMap.WorldToCell(mousePosition);

            blackTile = blackTileMap.GetTile(position);
            whiteTile = whiteTileMap.GetTile(position);
            
            if (blackTile != null)
            {
                if (dataFromTiles.ContainsKey(blackTile)) {
                    Debug.Log("Propagates?: " + dataFromTiles[blackTile].propagatesSignal +
                              "Is temp?:" + dataFromTiles[blackTile].isTemp);
                }
                Spread(position);
                PostSpread(position);
            }
            else if (whiteTile != null)
            {
                Debug.Log("White Tile: " + whiteTile.name);
                Spread(position);
                PostSpread(position);
            }
        }
    }


    /**
     * Call this after the spread was finished.
     */ 
    public void PostSpread(Vector3Int position)
	{
        activeTiles = new List<Vector3Int>();
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
            TileBase blackTile = blackTileMap.GetTile(position);
            TileBase whiteTile = whiteTileMap.GetTile(position);

            TileData data;

            if (!activeTiles.Contains(position))
            {

                if (blackTile != null &&
                    dataFromTiles.ContainsKey(blackTile))
                {
                    data = dataFromTiles[blackTile];

                    // Checkerboard spreading rule
                    if (data.isCheckerboard)
                    {
                        blackTileMap.SetTile(position, null);
                        whiteTileMap.SetTile(position, tempWhiteWall.tiles[0]); // Do the positions match?

                        activeTiles.Add(position);
                        Spread(position);
                    }
                    // Wire spreading rule
                    else if (data.isWire)
                    {
                        activeTiles.Add(position);
                        Spread(position);

                    }
                }
                // Switch ON/OFF
                else if (whiteTile != null &&
                    dataFromTiles.ContainsKey(whiteTile))
                {
                    data = dataFromTiles[whiteTile];

                    if (data.isTemp)
                    {
                        blackTileMap.SetTile(position, checkerboard.tiles[0]);
                        whiteTileMap.SetTile(position, null);
                        activeTiles.Add(position);
                        Spread(position);
                    }
                }
            }
        }
    }
}

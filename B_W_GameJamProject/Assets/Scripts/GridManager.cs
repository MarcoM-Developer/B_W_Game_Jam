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
    private TileData whiteWall;

    [SerializeField]
    private TileData blackWall;

    [SerializeField]
    private TileData blackSwitch;

    [SerializeField]
    private TileData whiteSwitch;

    [SerializeField]
    private GameObject blackPlayer;

    [SerializeField]
    private GameObject whitePlayer;

    public AudioManager audioManager; // TODO

    private Dictionary<TileBase, TileData> dataFromTiles;
    private List<Vector3Int> activeTiles = new List<Vector3Int>();


    /**
     * constants for what signal to spread
     */
    const string BLACK_SIGNAL = "blackSignal";
    const string WHITE_SIGNAL = "whiteSignal";

    /**
     * Populate the dataFromTiles, so I can fetch the tiles later
     */ 
    private void Awake()
	{
        dataFromTiles = new Dictionary<TileBase, TileData>();
        TileData[] tileData = { blackWire,
                                whiteWire,
                               // checkerboard,
                                whiteWall,
                                blackWall,
                                whiteSwitch,
                                blackSwitch};

            foreach(var data in tileData)
            { 
                if (data.tiles != null)
                {
                    foreach (var tile in data.tiles)
                    {
			if (tile != null) 
			{
			    dataFromTiles[tile] = data;
                            //dataFromTiles.Add(tile, data);
			}
                    }
                 }
			
             }
	}



    // TODO: Setup the dual map?
    void Start()
    {
	// A little spaghetti code here, but hey who cares.
	
	// 1. Generate a random white Tile map! 
	
	
	// 2. Given white Tile map, create a dual tilemap.
	whiteTileMap.CompressBounds();	// compress bounds, just in case.
	
	blackTileMap.origin = whiteTileMap.origin; // lock origin and size
	blackTileMap.size = whiteTileMap.size;
	blackTileMap.ResizeBounds();

	Debug.Log(dataFromTiles[whiteWall.tiles[1]]);
	Debug.Log(Array.FindIndex(whiteWall.tiles, x => (x == whiteWall.tiles[1]) ));

	// Create the dual:
	BoundsInt bounds = whiteTileMap.cellBounds;
	TileBase[] allTiles = whiteTileMap.GetTilesBlock(bounds);
	
	// Surek
	foreach (Vector3Int position in bounds.allPositionsWithin){

	        TileBase tile = whiteTileMap.GetTile(position);			
					
		if (tile != null && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].type == TileData.WHITEWALL) 
		{
				int index = Array.FindIndex(whiteWall.tiles, x => (x == tile) );
				TileBase dual = whiteWall.dualsTiles[index];

				blackTileMap.SetTile(position, dual);

		}else if(tile == null)
		{
				blackTileMap.SetTile(position, blackWall.tiles[0]);

		}
	}
    }


    // HACK: This should be done using events, but who cares, it's a GameJam.
    private bool whiteOnSwitch = false; 
    private bool blackOnSwitch = false;

    // TODO: Implement white wire controls.

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


        // TODO: De-Spaghetti this code ...
        // Check if White stands on switch.
        if (blackTile != null && dataFromTiles.ContainsKey(blackTile))
		{
            TileData data = dataFromTiles[blackTile];

            if (data.isSwitch)
			{
                // Yes, I White Player stand on switch.

                if (!whiteOnSwitch)
				{
                    //... and in fact, I just moved to the switch...
                    // Debug.Log("White Player entered switch.");

                    // ... close the switch
                    PlayFlickSwitchSound();
                    blackTileMap.SetTile(blackTilePosition, blackWire.tiles[0]);

                    //... tell the world I am standing on the switch! 
                    whiteOnSwitch = true;

                    //... spread the word.
                    Spread(blackTilePosition, WHITE_SIGNAL);
                    PostSpread(blackTilePosition);
                }
			}else
			{
                // No, I do not stand on switch.

                if (whiteOnSwitch)
				{
                    //... and truth be told, I just left the switch.
                    // Debug.Log("Exited switch.");

                    //... tell the world I shamelessly quit the switch!
                    whiteOnSwitch = false;

                    //... spread the word.
                    Spread(blackTilePosition, WHITE_SIGNAL);
                    PostSpread(blackTilePosition);

                }
			}
            //Debug.Log("Wire: "+data.isWire + "Switch: "+data.isSwitch);
		}

        //!!! May be wrong
        // TODO: Clean the code up, this is a organized mess.
        // BLACK PLAYER
        if (whiteTile != null &&
            dataFromTiles.ContainsKey(whiteTile))
        {
            TileData data = dataFromTiles[whiteTile];

            if (data.isSwitch)
            {
                // Yes, I Black Player stand on a switch.
                if (!blackOnSwitch)
                {
                    // ... I just moved to the switch
                    // Debug.Log("Black Player entered switch.");

                    // ... close the switch
                    PlayFlickSwitchSound();
                    whiteTileMap.SetTile(whiteTilePosition, whiteWire.tiles[0]);

                    //... tell the world that I am on the switch!
                    blackOnSwitch = true;

                    //... spread the signal;
                    Spread(whiteTilePosition, BLACK_SIGNAL);
                    PostSpread(whiteTilePosition);
                }
            }
            else
            {

                // No, I don't stand on a switch.
                if (blackOnSwitch)
                {
                    //... I just left the switch.
                    // Debug.Log("Black Exited Switch.");

                    blackOnSwitch = false;

                    Spread(whiteTilePosition, BLACK_SIGNAL);
                    PostSpread(whiteTilePosition);

                }
            }
        }

        // Mouse (for testing)
        /* if (Input.GetMouseButtonDown(0))
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
        }*/
    }


    private void PlayFlickSwitchSound()
	{
        AudioManager.instance.Play("WireSwitch");
        //FindObjectOfType<AudioManager>().Play("WireSwitch");
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
    public void Spread(Vector3Int position,
                       string blackOrWhiteSignal)
    {
        for (int x = position.x - 1; x < position.x + 2; x++)
        {
            for (int y = position.y - 1; y < position.y + 2; y++)
            {
                TryToSpreadTile(new Vector3Int(x, y, 0), blackOrWhiteSignal);
            }
        }


        void TryToSpreadTile(Vector3Int position, string blackOrWhiteSignal)
                            
        {
            TileBase blackTile = blackTileMap.GetTile(position);
            TileBase whiteTile = whiteTileMap.GetTile(position);

            TileData blackTileData;
            TileData whiteTileData;

            if (!activeTiles.Contains(position))
            {
                bool isWhiteTileAvailable = (whiteTile != null && dataFromTiles.ContainsKey(whiteTile));
                bool isBlackTileAvailable = (blackTile != null && dataFromTiles.ContainsKey(blackTile));

                if (isBlackTileAvailable && dataFromTiles[blackTile].propagatesSignal)
                {
                    blackTileData = dataFromTiles[blackTile];

                    // Checkerboard spreading rule
                    if (blackTileData.isCheckerboard)
                    {
                        switch (blackOrWhiteSignal)
                        {
                            case WHITE_SIGNAL: // White player sent the signal, put a white wall there.
                                blackTileMap.SetTile(position, null);
                                whiteTileMap.SetTile(position, whiteWall.tiles[0]); // Do the positions match?
                                break;

                            case BLACK_SIGNAL: // Black player sent the signal, put a black wall there.
                                whiteTileMap.SetTile(position, null);
                                blackTileMap.SetTile(position, blackWall.tiles[0]);
                                break;

                        }
                        activeTiles.Add(position);
                        Spread(position, blackOrWhiteSignal);
                    }
                    // Wire spreading rule
                    else if (blackTileData.isWire)
                    {
                        activeTiles.Add(position);
                        Spread(position, blackOrWhiteSignal);

                    }
                }
                // Spaghetti code attacks...
                else if (isWhiteTileAvailable && dataFromTiles[whiteTile].propagatesSignal)       
                {
                    whiteTileData = dataFromTiles[whiteTile];

                    // Checkerboard spreading rule
                    if (whiteTileData.isCheckerboard)
                    {
                        // (blackTile has precedence here btw.)
                        switch (blackOrWhiteSignal)
                        {
                            case WHITE_SIGNAL: // White player sent the signal, put a white wall there.
                                blackTileMap.SetTile(position, null);
                                whiteTileMap.SetTile(position, whiteWall.tiles[0]); // Do the positions match?
                                break;

                            case BLACK_SIGNAL: // Black player sent the signal, put a black wall there.
                                whiteTileMap.SetTile(position, null);
                                blackTileMap.SetTile(position, blackWall.tiles[0]);
                                break;

                        }
                        activeTiles.Add(position);
                        Spread(position, blackOrWhiteSignal);
                    }
                    // Wire spreading rule
                    else if (whiteTileData.isWire)
                    {
                        activeTiles.Add(position);
                        Spread(position, blackOrWhiteSignal);

                    }
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombExplode: MonoBehaviour
{
    public Player player;
    [SerializeField] private int bombDelay;

    // TODO: turn this into SerializedFields?
    public Tilemap whiteTileMap;
    public Tilemap blackTileMap;
    
    public TileData whiteWall;
    public TileData blackWall;

    public bool isWhite;
    
    private int bombTimer = 0;
    public Tilemap destructibleTileMap; // tiles to destroy
    public Tilemap backgroundTileMap; // tiles to avoid
    public TileData destructibleTileData; // tiles to destroy
    public TileData backgroundTileData;

    private int blastRadius = 2; // Bomb blast radius
    
    
        // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
	bombTimer += 1;
	if (bombTimer > bombDelay)
	{
		// Explode here
		Debug.Log("yup");

		// Destroy the terrain;
		Vector2 bombPosition = this.transform.position;
		Debug.Log(backgroundTileMap);
		Vector3Int bombTilePosition = backgroundTileMap.WorldToCell(bombPosition);

		// TEST
		// Square blast first
		for (int x = -blastRadius; x < blastRadius; x++){
			for (int y = -blastRadius; y < blastRadius; y++) {
			  	Vector3Int delta = new Vector3Int(x,y,0);
				Vector3Int position = bombTilePosition + delta;		
				
				TileBase backgroundTile = backgroundTileMap.GetTile(position);
				TileBase destructibleTile = destructibleTileMap.GetTile(position);

				Debug.Log(backgroundTile);
				Debug.Log(destructibleTile);
				
				if(backgroundTile.name != "checkerboard" &&
				   destructibleTile.name != "checkerboard"){
					backgroundTileMap.SetTile(position, backgroundTileData.tiles[0]);
					destructibleTileMap.SetTile(position, null); //destructibleTileData.tiles[0]);
				}
			}
		}
		// backgroundTileMap.SetTile(bombTilePosition, null);
		//destructibleTileMap.SetTile(bombTilePosition,  destructibleTileData.tiles[0]);

		// CODE UP EXPLOSIONS HERE: 
		


		// Just to try here:
		Destroy(gameObject);
		

		// Kill the player if near
	}
    }

    private void FixedUpdate()
    {
	    // HUH
    }
}

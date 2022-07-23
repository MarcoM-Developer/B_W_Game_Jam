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
    public TileData backgroudTileData;
    
    
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

		backgroundTileMap.SetTile(backgroundTileMap.WorldToCell(bombPosition), destructibleTileData.tiles[0]);
		destructibleTileMap.SetTile(destructibleTileMap.WorldToCell(bombPosition), destructibleTileData.dualsTiles[0]);

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

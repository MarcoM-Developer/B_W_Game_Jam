using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombDrop: MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private Player player;
    [SerializeField] private int initialNumberBombs;
    [SerializeField] private GameObject bombPrefab;

    public Tilemap whiteTileMap;
    public Tilemap blackTileMap;

    
    [SerializeField]
    private TileData whiteWall;

    [SerializeField]
    private TileData blackWall;


    public Rigidbody2D PlayerRigidBody { get => playerRigidBody; set => playerRigidBody = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.isActive)
	{
		if (initialNumberBombs > 0) {
			initialNumberBombs -= 1;
			Debug.Log("instantiated");
			GameObject bomb = Instantiate(bombPrefab); 
			bomb.transform.position = PlayerRigidBody.position;

			// Setup:
			BombExplode explode = bomb.GetComponent<BombExplode>();
			explode.whiteTileMap = whiteTileMap;
			explode.blackTileMap = blackTileMap;

			// Spaghetti code :)
			if (explode.isWhite) 
			{
				explode.destructibleTileMap = whiteTileMap;
				explode.backgroundTileMap = blackTileMap; // if white
				explode.destructibleTileData = whiteWall;
				explode.backgroudTileData = blackWall;
			} else {
				explode.destructibleTileMap = blackTileMap;
				explode.backgroundTileMap = whiteTileMap;
				explode.destructibleTileData = blackWall;
				explode.backgroudTileData = whiteWall;
			}	
	
		}
	}
    }

    private void FixedUpdate()
    {
	    // HUH
    }
}

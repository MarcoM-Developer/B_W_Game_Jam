using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpreadWhite : MonoBehaviour
{
    private Vector3Int position;
    private TileData tileData;
    private GridManager gridManager;
    private float burnTimeCounter, spreadIntervalCounter;
    private const float deltaTime = 0.1f;
 
    /**
     * StartSpread
     *
     * spreads white tiles around position.
     */ 
    public void StartSpread(Vector3Int position,
                            TileData data,
                            GridManager gm)
    { 
        this.position = position;
        this.tileData = data;
        gridManager = gm;

        burnTimeCounter = data.burnTime;
        spreadIntervalCounter = data.spreadInterval;
	}


    private void Update()
	{
        burnTimeCounter -= deltaTime; // <--- Implement this cleaner.

        if (burnTimeCounter <= 0)
		{
			//gridManager.FinishedSpread(position);
            Destroy(gameObject);
		}

        spreadIntervalCounter -= deltaTime;

        if (spreadIntervalCounter <= 0)
		{
            spreadIntervalCounter = tileData.spreadInterval;
            //gridManager.TryToSpread(position, tileData.spreadChance);
		}
	}
}

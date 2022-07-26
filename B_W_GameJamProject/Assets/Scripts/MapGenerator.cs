using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    // Generate Random Maps :) 
    
    [SerializeField] private Tilemap blackTileMap;
    [SerializeField] private Tilemap whiteTileMap;
    [SerializeField] private GameObject blackPlayer;
    [SerializeField] private GameObject whitePlayer;
    [SerializeField] private GameObject whiteGoal;
    [SerializeField] private GameObject blackGoal;


    // Map block 
    private int MAP_BLOCK_SIZE = 12;
    private int LEVEL_SIZE = 5;

    // Similarly to Spelunky, 
    //
    // 1) Start every level at the top.
    // 2) Pick a starting site.
    // 3) Create a Path.
    //
    // 
    // 

    // Start is called before the first frame update
    void Start()
    {
	// Randomly pick start, midpoint and finish 
	// and connect them with shortest paths
        int[,] level = new int[LEVEL_SIZE, LEVEL_SIZE];
	

	// Simple breadth-first search. 
	// This will be fine here, but slow on bigger grids.
	List<Vector3Int> path = generateLevelPath();

    }


    List<Vector3Int> generateLevelPath()
    {
	  System.Random random = new System.Random();
	
	  int s = random.Next(2); // Initial block in the first row
	  int f = random.Next(2); // Final block in the last row.
 	  int m = 2-(s+f); 

	  Vector3Int start  = new Vector3Int(s*(LEVEL_SIZE-1), 0, 0);
      	  Vector3Int mid    = new Vector3Int(m*(LEVEL_SIZE-1)/2, (LEVEL_SIZE-1)/2, 0);
	  Vector3Int finish = new Vector3Int(f*(LEVEL_SIZE-1), LEVEL_SIZE-1, 0);


	  Debug.Log("start: "+start.ToString());
	  Debug.Log("mid: "+ mid.ToString());
	  Debug.Log("finish: "+finish.ToString());


	  List<Vector3Int> path1 = getPath(start, mid + Vector3Int.down);
	  List<Vector3Int> path2 = getPath(mid + Vector3Int.up, finish);
	  path2.Add(mid); 

	  List<Vector3Int> path = path2.Concat(path1).ToList();
	  Debug.Log("path:"+path.Count);

	  return path;
    }


    List<Vector3Int> getPath(Vector3Int start, Vector3Int finish)
    {
	    /**
	     * Returns a path from finish to start
	     */ 
	    
	    System.Random random = new System.Random();

	    Queue<Vector3Int> queue = new Queue<Vector3Int>();
	    queue.Enqueue(start);

	    
	    int L = LEVEL_SIZE*LEVEL_SIZE;
	    int[,] explored = new int[LEVEL_SIZE, LEVEL_SIZE];
		
	    // Populate with L
	    for (int col = 0; col < explored.GetLength(0); col++){
		    for( int row = 0; row < explored.GetLength(1); row++) {
			    explored[col, row] = L;
		    }
	    }

	    explored[start.x, start.y] = 1; // label start as explored.

	    // Populate explored
	    while (queue.Count > 0)
	    { // If there is any element in the list.
		   Vector3Int q = queue.Dequeue();
 		   //Debug.Log("q: "+q.ToString());
		   //Debug.Log("q value: "+explored[q.x, q.y]);

		   if (q == finish) 
		   {
			   //Debug.Log(finish.ToString());
			   //Debug.Log("q is finish");
			   break;
		   }

		   List<Vector3Int> neighbors = getNeighbors(q);

		   foreach(Vector3Int neighbor in neighbors) 
		   {
			   if (explored[neighbor.x, neighbor.y] == L) 
			   {
				   explored[neighbor.x, neighbor.y] = explored[q.x, q.y] + 1;
				   queue.Enqueue(neighbor);
			   }
		   }
	    }

	    Debug.Log("finish value: "+explored[finish.x, finish.y]);


	    // Reconstruct the path
	    List<Vector3Int> path = new List<Vector3Int>();
	    path.Add(finish);

	    int val = explored[finish.x, finish.y];
	    
	    while (true) {
		    List<Vector3Int> neighbors = getNeighbors(path.Last());
		    List<Vector3Int> nextSteps = new List<Vector3Int>();

		    foreach(Vector3Int neighbor in neighbors)
		    {
			    Debug.Log(explored[neighbor.x, neighbor.y] );
			    if (explored[neighbor.x, neighbor.y] < val)
			    {
				    nextSteps.Add(neighbor);
			    }
		    }



		    Vector3Int nextStep = nextSteps[random.Next(nextSteps.Count)];
		    path.Add(nextStep);
		    
		    val = explored[nextStep.x, nextStep.y];

		    if (nextStep == start){
			    break;
		    }

	    }

	    return path;
    }

    
    List<Vector3Int> getNeighbors(Vector3Int node)
    {
	    /*
	     * Get all neighboring sites of a fixed site.
	     */
	    
	    List<Vector3Int> neighbors = new List<Vector3Int>();
	    
	    if (node.x > 0){
		    neighbors.Add(node + Vector3Int.left);
	    }
	    
	    if (node.x < LEVEL_SIZE-1) 
	    {
		    neighbors.Add(node + Vector3Int.right);
	    }
	    
	    if (node.y > 0) 
	    {
		    neighbors.Add(node + Vector3Int.down);
	    }
	    
	    if (node.y < LEVEL_SIZE-1)
	    {
		    neighbors.Add(node + Vector3Int.up);
	    }

	    Debug.Log(neighbors);

	    return neighbors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

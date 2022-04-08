using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBlock : MonoBehaviour
{
    /**
     * Put the finish block onto the two respective
     * terminal positions. The script keeps a counter
     * of how many triggers were there and calls ... whe all is done.
     */

    public delegate void OnFinished();
    public static OnFinished onFinished;

    private static int instanceTriggerCount = 0; // how many times were this triggered in a game



    // Start is called before the first frame update (on a scene?)
    void Start()
    {
        instanceTriggerCount = 0; // set zero trigger in the beggining.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        //Debug.Log("Goal collided with " + other.name + " with tag " + other.tag);

        if (other.tag == "Player")
		{
           
            instanceTriggerCount++;

			if (instanceTriggerCount == 2)
			{
                Debug.Log("Both picked");

                if (onFinished != null) {
                    //onFinished(); 
                }
            }

			Destroy(gameObject); // Remove the game object.
        }
		
    }
}

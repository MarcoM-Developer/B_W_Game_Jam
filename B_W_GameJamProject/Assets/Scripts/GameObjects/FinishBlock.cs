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

    [SerializeField] private BoolReference isPickedUp;
    public delegate void OnFinished();
    public static OnFinished onFinished;

    public static int instanceTriggerCount = 0; // how many times were this triggered in a game


    private void OnEnable()
    {
        SaveManager.OnLoad += LoadValues;
    }

    // Start is called before the first frame update (on a scene?)
    void Start()
    {
        instanceTriggerCount = 0; // set zero trigger in the beggining.

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        SaveManager.OnLoad -= LoadValues;
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        //Debug.Log("Goal collided with " + other.name + " with tag " + other.tag);

        if (other.tag == "Player")
		{
           
            instanceTriggerCount++;
            isPickedUp.Value = true;
            
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

    private void LoadValues()
    {
        if (isPickedUp.Value)
        {
            instanceTriggerCount++;
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        if (instanceTriggerCount == 2)
        {
            Debug.Log("Both picked");

            if (onFinished != null)
            {
                //onFinished(); 
            }
        }
    }

}

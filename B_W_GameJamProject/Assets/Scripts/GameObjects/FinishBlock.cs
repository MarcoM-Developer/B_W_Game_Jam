using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishBlock : MonoBehaviour
{
    /**
     * Put the finish block onto the two respective
     * terminal positions. The script keeps a counter
     * of how many triggers were there and calls ... whe all is done.
     */

    [SerializeField] private BoolReference isPickedUpRef;
    private bool isPickedUp;


    public delegate void LevelEnded(int sceneBuildIndex);
    public static LevelEnded OnFinished;

    public static int instanceTriggerCount = 0; // how many times were this triggered in a game


    private void OnEnable()
    {
        SaveManager.OnSave += SaveValues;
        SaveManager.OnLoad += LoadValues;
    }

    // Start is called before the first frame update (on a scene?)
    void Start()
    {
        instanceTriggerCount = 0; // set zero trigger in the beggining.
    }

    private void OnDisable()
    {
        SaveManager.OnSave -= SaveValues;
        SaveManager.OnLoad -= LoadValues;
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        //Debug.Log("Goal collided with " + other.name + " with tag " + other.tag);

        if (other.tag == "Player")
		{
           
            instanceTriggerCount++;
            isPickedUp = true;
            
			if (instanceTriggerCount == 2)
			{
                Debug.Log("Both picked");

                if (OnFinished != null) {
                    OnFinished(SceneManager.GetActiveScene().buildIndex+1); 
                }
            }

			Destroy(gameObject); // Remove the game object.
        }
		
    }

    private void LoadValues()
    {
        if (isPickedUpRef.Value)
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

            if (OnFinished != null)
            {
                OnFinished(SceneManager.GetActiveScene().buildIndex+1); 
            }
        }
    }

    private void SaveValues()
    {
        isPickedUpRef.Value = isPickedUp;
    }


}

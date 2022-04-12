using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Audio_White_Player : MonoBehaviour
{

    Rigidbody2D rigidBody;
    LayerMask layerMask;
    private float pitchOffset = 0;

    public void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
     
        AudioManager.instance.Play("PlayerMove");
        gameObject.layer = layerMask;

        if (layerMask == 9)
            pitchOffset = -0.2f;
        else 
            pitchOffset = 0;
    }

    public void Update()
    {
        if (!rigidBody)
        {
            return; // If you don't have a rigid body, leave the scene quietly.
        }
        float speed = Mathf.Abs(rigidBody.velocity.x); // Check if this works.

       
        

        float adjVolRange = Mathf.Lerp(0f, 1f, speed);
        float adjPitchRange = Mathf.Lerp(1f,2f,speed);

        //AudioManager.instance.VolumeAdjust(adjVolRange);
       // AudioManager.instance.PitchAdjust(adjPitchRange + pitchOffset);


    }//end update





}//END MAIN
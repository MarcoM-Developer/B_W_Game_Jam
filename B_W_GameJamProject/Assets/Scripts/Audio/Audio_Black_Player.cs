using UnityEngine;


public class Audio_Black_Player : MonoBehaviour
{

    Rigidbody2D rigidBody;

    private float pitchOffset = 0;

    public void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();


        AudioManager.instance.Play("PlayerBlackMove");


    }

    public void Update()
    {
        if (!rigidBody)
        {
            return; // If you don't have a rigid body, leave the scene quietly.
        }
        float speed = Mathf.Abs(rigidBody.velocity.x); // Check if this works.




        float adjVolRange = Mathf.Lerp(0f, 1f, speed);
        float adjPitchRange = Mathf.Lerp(1f, 2f, speed);

        AudioManager.instance.sounds[4].volume = adjVolRange;
        AudioManager.instance.sounds[4].pitch = adjPitchRange + pitchOffset;
 



    }//end update





}//END MAIN
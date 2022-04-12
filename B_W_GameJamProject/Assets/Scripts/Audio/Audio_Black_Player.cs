using UnityEngine;


public class Audio_Black_Player : MonoBehaviour
{

    Rigidbody2D rigidBody;
 

    private float pitchOffset = 0;
    

    public void Start()
    {
        
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        AudioManager.instance.Play("BlackRoll");
    }

    public void Update()
    {
        Sound playMove = AudioManager.instance.GetSound("BlackRoll");
        if (!rigidBody)
        {
            return; // If you don't have a rigid body, leave the scene quietly.
        }
        float speed = Mathf.Abs(rigidBody.velocity.x); // Check if this works.



        float adjVolRange = Mathf.Lerp(0f, 1f, speed);
        print("BLACK Speed: " + speed + " Adjusted: " + adjVolRange);

        float adjPitchRange = Mathf.Lerp(1f, 2f, speed);
        playMove.source.pitch = adjVolRange;
        playMove.source.volume = adjPitchRange + pitchOffset;
 



    }//end update





}//END MAIN
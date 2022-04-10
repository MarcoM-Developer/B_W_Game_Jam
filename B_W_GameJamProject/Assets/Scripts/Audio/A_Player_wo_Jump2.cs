using System;
using UnityEngine;

/*
Sounds that the player makes

Sounds:
-Footsteps
-Jump
-Land
-Basic Attack
-Fire/Water/Electrical Attacks
---Small
---Charge
-Take Damage
--Generic
--Fire
--Electrical
RTPCs:
--Health
States:
-Surface type
*/
public class A_Player_wo_Jump2 : MonoBehaviour
{

    //This is for a single player. Items should be generic for each player

    //Things to be accessed by anyone

    Rigidbody2D rigidBody; 

    //Things to be acecessed by only this script

    [SerializeField] private AK.Wwise.Event pMoveLR;
    [SerializeField] private AK.Wwise.Event pStopLR;
    [SerializeField] private AK.Wwise.RTPC rtpcPlayerSpeed;



    private bool isSoundPlaying = false;



    public void Start() 
    {
	rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {

        NoJumpPlayerMotionLR();

 

        //PlayerLand();



    }//end update



    private void NoJumpPlayerMotionLR()
    {
	if (!rigidBody) {
		return; // If you don't have a rigid body, leave the scene quietly.
	}

	float speed = Mathf.Abs(rigidBody.velocity.x); // Check if this works.
     //   Debug.Log(speed);

	rtpcPlayerSpeed.SetValue(gameObject, speed);
	if (speed >= 1)
        {
            pMoveLR.Post(gameObject);
            isSoundPlaying = true;
            Debug.Log("Sound is Playing");
        }
        else if (speed < 1)
        {
            if (isSoundPlaying)
            {
                pStopLR.Post(gameObject);
                isSoundPlaying = false;
                Debug.Log("Sound is Not Playing");
            }
        }
    }


/*    private void PlayerLand()
    {
        if (sTerrain.playerLanded)
            pLand.Post(gameObject);

    }*/




}//END MAIN

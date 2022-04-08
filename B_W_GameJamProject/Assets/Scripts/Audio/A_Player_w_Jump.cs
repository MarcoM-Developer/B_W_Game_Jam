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
public class A_Player_w_Jump : MonoBehaviour
{

    //Things to be accessed by anyone
    #region Public Variables
    public Player sWhitePlayer;//get player script
    public Player sBlackPlayer;//get player script
    public PlayerStateManager playerStateManager;


    public Movement sMovement;//get movement script
    public Jump sJump;
    public TerrainDetection sTerrain;
    //public GameObject playerGameObject;
    public Rigidbody2D sRigidbody;
    #endregion

    //Things to be acecessed by only this script
    #region Private Variables
    [Header("Locomotion")]
    [SerializeField] private AK.Wwise.Event pWhiteMoveLR;
    [SerializeField] private AK.Wwise.Event pBlackMoveLR;
    [SerializeField] private AK.Wwise.Event pWhiteStopLR;
    [SerializeField] private AK.Wwise.Event pBlackStopLR;
    [SerializeField] private AK.Wwise.Event plyrJump;
    [SerializeField] private AK.Wwise.Event pLand;

    private bool isSoundPlaying = false;
    private float currentVelocity;
    #endregion

    public void Update()
    {


        PlayerMotionLR();

        PlayerJump();

       // PlayerLand();

    }//end update




    private void PlayerMotionLR()
    {
        //Player Movement/Speed
        AkSoundEngine.SetRTPCValue("PlayerSpeed", GetCurrentVelocity());

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (!isSoundPlaying)//if no sound is playing
            {

               if(playerStateManager.CurrentState)
                    

                if (sWhitePlayer.gameObject.layer == 8) //white
                    pWhiteMoveLR.Post(gameObject);
                else if (sWhitePlayer.gameObject.layer == 9) //black
                    pBlackMoveLR.Post(gameObject);
                isSoundPlaying = true;
            }

        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (isSoundPlaying)//if sound is playing
            {

                if (sWhitePlayer.gameObject.layer == 9) // not white
                    pWhiteStopLR.Post(gameObject);
                else if (sWhitePlayer.gameObject.layer == 8) //not black
                    pBlackStopLR.Post(gameObject);
                isSoundPlaying = false;


            }
        }
    }

    private void PlayerJump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //play jump
            plyrJump.Post(gameObject);
        }

    }


    private void PlayerLand()
    {
        if (sTerrain.playerLanded)
            pLand.Post(gameObject);

    }

    public float GetCurrentVelocity()
    {

        if (sRigidbody.velocity.x < 0)
            currentVelocity = -sRigidbody.velocity.x;
        else
            currentVelocity = sRigidbody.velocity.x;

        return currentVelocity;
    }





}//END MAIN
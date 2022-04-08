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
public class A_Player_Jump : MonoBehaviour
{

    //Things to be accessed by anyone
    #region Public Variables
    public Player sPlayer;//get player script
    public Movement sMovement;//get movement script
    public Jump sJump;
    public TerrainDetection sTerrain;
    //public GameObject playerGameObject;
    public Rigidbody sRigidbody;
    #endregion

    //Things to be acecessed by only this script
    #region Private Variables
    [Header("Locomotion")]
    [SerializeField] private AK.Wwise.Event pMoveLR;
    [SerializeField] private AK.Wwise.Event pStopLR;
    [SerializeField] private AK.Wwise.Event plyrJump;
    [SerializeField] private AK.Wwise.Event pLand;
 
    [SerializeField] private AK.Wwise.Event pWallCollide;
    private bool isSoundPlaying = false;
    private float currentVelocity;
    #endregion

    public void Update()
    {
        //get and set player health RTPC
        //AkSoundEngine.SetRTPCValue("PlayerSpeed", pMovement);
        SetPlayerState();

        PlayerMotionLR();
       
        PlayerJump();

        PlayerLand();

        PlayerWallCollision();


    }//end update

    private void PlayerLand()
    {
        if(sTerrain.playerLanded)
            pLand.Post(gameObject);
 
    }

    private void PlayerMotionLR()
    {
        //Player Movement/Speed
        AkSoundEngine.SetRTPCValue("PlayerSpeed", GetCurrentVelocity());

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (!isSoundPlaying)
            {
                pMoveLR.Post(gameObject);
                isSoundPlaying = true;
            }

        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (isSoundPlaying)
            {
                pStopLR.Post(gameObject);
                isSoundPlaying = false;
                print("Stop");
            }
        }
    }
    private void SetPlayerState()
    {
        //Player Type Swap State
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //set player state B or W
            if (sPlayer.gameObject.layer == 8)
                AkSoundEngine.SetState("CurrentPlayer", "White");
            else if (sPlayer.gameObject.layer == 9)
                AkSoundEngine.SetState("CurrentPlayer", "Black");

        }
    }

    private void PlayerWallCollision()
    {
        switch (sPlayer.PlayerType)
        {
            case PlayerType.White:
                sTerrain.layerTarget = LayerMask.GetMask("WhiteWalls");
                //play wall sound
                pWallCollide.Post(gameObject);
                break;

            case PlayerType.Black:
                sTerrain.layerTarget = LayerMask.GetMask("BlackWalls");
                //play wall sound
                pWallCollide.Post(gameObject);
                break;
        }
    }

    public float GetCurrentVelocity()
    {

        if (sRigidbody.velocity.x < 0)
            currentVelocity = -sRigidbody.velocity.x;
        else
            currentVelocity = sRigidbody.velocity.x;

        return currentVelocity;
    }

    private void PlayerJump()
    {
        if (sJump.canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //play jump
                plyrJump.Post(gameObject);
            }
        }
    }



}//END MAIN
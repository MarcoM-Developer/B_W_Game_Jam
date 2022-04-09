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
public class A_Player_wo_Jump : MonoBehaviour
{

    //This is for a single player. Items should be generic for each player

    //Things to be accessed by anyone
    #region Public Variables
    public Player sCurrentPlayer;//get player script
    public PlayerStateManager playerStateManager;
    public Movement sMovement;//get movement script
    public TerrainDetection sTerrain;
    public Rigidbody2D sRigidbody;
    #endregion

    //Things to be acecessed by only this script
    #region Private Variables
    [Header("Locomotion")]
    [SerializeField] private AK.Wwise.Event pMoveLR;
    [SerializeField] private AK.Wwise.Event pStopLR;
    [SerializeField] private AK.Wwise.Event pLand;

    private bool isSoundPlaying = false;
    private float currentVelocity;
    #endregion

    public void Update()
    {
        if (sCurrentPlayer.gameObject.layer == 8)
        { //white
            AkSoundEngine.SetState("CurrentPlayer", "White");

        }
        else if (sCurrentPlayer.gameObject.layer == 9)
        {  //black
            AkSoundEngine.SetState("CurrentPlayer", "Black");
        }


        NoJumpPlayerMotionLR();

        //PlayerJump();

        // PlayerLand();

    }//end update




    private void NoJumpPlayerMotionLR()
    {
        //Player Movement/Speed
        AkSoundEngine.SetRTPCValue("PlayerSpeed", GetCurrentVelocity());

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (!isSoundPlaying)//if no sound is playing
            {

                if (playerStateManager.CurrentState)


                    if (sCurrentPlayer.gameObject.layer == 8)
                    { //white
                        pMoveLR.Post(gameObject);
                    }
                    else if (sCurrentPlayer.gameObject.layer == 9)
                    {  //black
                        pMoveLR.Post(gameObject);
                    }
                isSoundPlaying = true;
            }

        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (isSoundPlaying)//if sound is playing
            {

                if (sCurrentPlayer.gameObject.layer == 9) // not white
                    pStopLR.Post(gameObject);
                else if (sCurrentPlayer.gameObject.layer == 8) //not black
                    pStopLR.Post(gameObject);
                isSoundPlaying = false;


            }
        }
    }

    /*   private void PlayerJump()
       {

           if (Input.GetKeyDown(KeyCode.Space))
           {
               //play jump
               plyrJump.Post(gameObject);
           }

       }*/


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
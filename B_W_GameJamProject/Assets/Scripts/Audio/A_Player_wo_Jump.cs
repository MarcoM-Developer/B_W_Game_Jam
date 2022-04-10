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
    [SerializeField] public Player sCurrentPlayer;//get player script
    public PlayerState sCurrentState;
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
    //[SerializeField] private AK.Wwise.State pTypeState;

    private bool isSoundPlaying = false;
    private float currentVelocity;
    #endregion

    public void Start()
    {
 

    }

    public void Update()
    {
        if (sCurrentState.enabled)
        {
            float currentSpeed = Mathf.Abs(sRigidbody.velocity.x);
            NoJumpPlayerMotionLR(currentSpeed);
        }

        
        
        //PlayerJump();

        //PlayerLand();

    }//end update




    private void NoJumpPlayerMotionLR(float mySpeed)
    {
        //Player Movement/Speed
        AkSoundEngine.SetRTPCValue("PlayerSpeed", mySpeed);

        //if moving in any direction play movement
        //if not focus play movemnt at a lower sound
        Debug.Log(currentVelocity);

        if (currentVelocity >= 1)
        {
            pMoveLR.Post(gameObject);
            isSoundPlaying = true;
            Debug.Log("Sound is Playing");
        }
        else if (currentVelocity < 1)
        {
            if (isSoundPlaying)
            {
                pStopLR.Post(gameObject);
                isSoundPlaying = false;
                Debug.Log("Sound is Not Playing");
            }
        }








        /*
                if (sCurrentPlayer.gameObject.layer == 8)
                { //white
                    AkSoundEngine.SetState("CurrentPlayer", "White");
                    pMoveLR.Post(gameObject);
                }
                else if (sCurrentPlayer.gameObject.layer == 9)
                {  //black
                    AkSoundEngine.SetState("CurrentPlayer", "Black");
                    pMoveLR.Post(gameObject);
                }*/


    }


    private void PlayerLand()
    {
        if (sTerrain.playerLanded)
            pLand.Post(gameObject);

    }

  


    //if speed over x value play sound


}//END MAIN
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
public class A_Player_wo_Jump : MonoBehaviour
{

    //Things to be accessed by anyone
    #region Public Variables
    public Player sPlayer;//get player script
    public Movement sMovement;//get movement script
    public TerrainDetection sTerrain;
    public Rigidbody2D sRigidbody;
    #endregion

    //Things to be acecessed by only this script
    #region Private Variables
    [Header("Locomotion")]
    [SerializeField] private AK.Wwise.Event pMoveLR;
    [SerializeField] private AK.Wwise.Event pStopLR;
    [SerializeField] private AK.Wwise.Event plyrJump;
    [SerializeField] private AK.Wwise.Event pLand;
    private bool isSoundPlaying = false;
    private float currentVelocity;
    #endregion

    public void Update()
    {


        PlayerMotionLR();

        // PlayerJump();

        PlayerLand();



    }//end update

    private void PlayerLand()
    {
        if (sTerrain.playerLanded)
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


    public float GetCurrentVelocity()
    {

        if (sRigidbody.velocity.x < 0)
            currentVelocity = -sRigidbody.velocity.x;
        else
            currentVelocity = sRigidbody.velocity.x;

        return currentVelocity;
    }





}//END MAIN
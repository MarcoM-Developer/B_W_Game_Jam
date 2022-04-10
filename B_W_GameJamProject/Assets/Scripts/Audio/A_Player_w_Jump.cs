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
    Rigidbody2D rigidBody;

    //Things to be acecessed by only this script
    #region Private Variables
    [Header("Locomotion")]
    [SerializeField] private AK.Wwise.Event pMoveLR;
    [SerializeField] private AK.Wwise.Event pStopLR;
    [SerializeField] private AK.Wwise.Event pLand;

    private bool isSoundPlaying = false;
    private float currentVelocity;
    #endregion

    public void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Update()
    {

        PlayerMotionLR();

        if (Input.GetKeyDown(KeyCode.Space))
            AkSoundEngine.PostEvent("Play_Plyr_Jump", gameObject);

        // PlayerLand();

    }//end update




    private void PlayerMotionLR()
    {
        if (!rigidBody)
        {
            Debug.Log("Nay, no rigid bodies today!");
            return; // If you don't have a rigid body, leave the scene quietly.
        }


        Debug.Log("Yay, I have a body and it's pretty damn rigid!");
        float speed = Mathf.Abs(rigidBody.velocity.x); // Check if this works.

        AkSoundEngine.SetRTPCValue("PlayerSpeed", speed);
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







}//END MAIN
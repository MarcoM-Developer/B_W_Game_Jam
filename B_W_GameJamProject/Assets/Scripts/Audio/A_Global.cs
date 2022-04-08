using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Global : MonoBehaviour
{
    public Player sWhitePlayer;//get player script
    public Player sBlackPlayer;//get player script


    [SerializeField] private AK.Wwise.Event pSwitchChars;
    [SerializeField] private AK.Wwise.Event playMusic;

    public void Start()
    {
        playMusic.Post(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

            //Player Type Swap State
            if (Input.GetKeyDown(KeyCode.Q))
            {
                pSwitchChars.Post(gameObject);
                //set player state B or W
                if (sWhitePlayer.gameObject.layer == 8)
                    AkSoundEngine.SetState("CurrentPlayer", "White");
                else if (sBlackPlayer.gameObject.layer == 9)
                    AkSoundEngine.SetState("CurrentPlayer", "Black");

            }
        }
    
}

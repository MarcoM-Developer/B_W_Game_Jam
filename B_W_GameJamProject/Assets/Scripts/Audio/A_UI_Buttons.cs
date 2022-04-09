using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_UI_Buttons : MonoBehaviour
{
public void PlayRollOver()
    {
        AkSoundEngine.PostEvent("Play_UI_MouseOver", gameObject);
    }

    public void PlayMousePress()
    {
        AkSoundEngine.PostEvent("Play_UI_Select", gameObject);
    }
}

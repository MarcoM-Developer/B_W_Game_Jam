using UnityEngine;

public class A_Items : MonoBehaviour
{
    //use to play sound when item is picked up
  

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "PlayerWhite" || other.name == "BlackPlayer")
        {
            AkSoundEngine.PostEvent("Play_Itm_Pu_Generic", gameObject);
        }

    }


}

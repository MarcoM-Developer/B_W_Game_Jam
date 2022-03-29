using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRotator : MonoBehaviour
{

    public delegate void MapRotate();
    public static event MapRotate RotateMap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (RotateMap != null)
            {
                RotateMap();
            }
        }
    }
}

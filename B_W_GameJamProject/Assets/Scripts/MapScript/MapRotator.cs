using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRotator : MonoBehaviour
{
    [SerializeField] private FloatReference byAngle;

    public delegate void MapRotate();
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RotateMap(byAngle.Value);
        }
    }
}

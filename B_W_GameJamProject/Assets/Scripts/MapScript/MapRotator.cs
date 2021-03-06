using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRotator : MonoBehaviour
{
    [SerializeField] private FloatReference byAngle;
    [SerializeField] private BoolReference isMapRotating;
    private MapRotation mapRotationScript;

    public delegate void MapRotate(float angle, Transform center);

    public static event MapRotate OnMapRotate;

    private void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // RotateMap(byAngle.Value);
            Debug.Log("Collision with player, now the map will rotate");

            if (!isMapRotating.Value)
            {
                gameObject.SetActive(false);

                if (OnMapRotate != null)
                {
                    OnMapRotate(byAngle.Value, transform);
                }
            }
        }
    }
}

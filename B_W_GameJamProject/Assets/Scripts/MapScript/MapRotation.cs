using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapRotation : MonoBehaviour
{
    [SerializeField] private FloatReference byAngle;
    [SerializeField] private FloatReference waitTimeForCoroutineRotateAroundPLayer;
    [SerializeField] private RotationCenter rotationCenter;

    private Player[] players;

    private float animationTime = 20;
    private float rotationSpeed = 100;

    private Transform playerTransform;

    private bool isAnimationRunning;
    private float currentRotationValue;


    private void OnEnable()
    {
        //MapRotator.RotateMap += RotateMap;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Map Rotation Script Loaded");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key down");
            RotateMap(byAngle.Value);
        }
        
    }
    private void OnDisable()
    {
       // MapRotator.RotateMap -= RotateMap;
    }

    private void RotateMap(float byAngle)
    {
        switch (rotationCenter)
        {
            case RotationCenter.Self:
                if (!isAnimationRunning)
                {
                    isAnimationRunning = true;
                    transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + byAngle), animationTime).OnComplete(() => { isAnimationRunning = false; });
                }
                break;

            case RotationCenter.Player:

               if (!isAnimationRunning)
               {
                    StartCoroutine(CRotateAroundPlayer(byAngle));
               }
               break;
        }
        
    }

    private IEnumerator CRotateAroundPlayer(float byAngle)
    {
        players = FindObjectsOfType<Player>();
        currentRotationValue = transform.eulerAngles.z;
        
        foreach (var player in players)
        {
            Debug.Log(player);
            if (player.IsActive)
            {
                playerTransform = player.transform;
            }
        }

        isAnimationRunning = true;

        while (isAnimationRunning)
        {
            yield return new WaitForSecondsRealtime(waitTimeForCoroutineRotateAroundPLayer.Value);
            
            transform.RotateAround(playerTransform.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
            
            if (currentRotationValue == 270)
            {
                if (transform.eulerAngles.z < 10)
                {
                    isAnimationRunning = false;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                }
            }
            else
            {
                if (transform.eulerAngles.z >= currentRotationValue + byAngle)
                {
                    isAnimationRunning = false;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRotationValue + byAngle);
                }
            } 
        }
    }
}


public enum RotationCenter
{
    Player,
    Self
}
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapRotation : MonoBehaviour
{
    [SerializeField] private FloatReference angleToAddForRotation;
    [SerializeField] private FloatReference animationTime;
    [SerializeField] private FloatReference rotationSpeed;
    [SerializeField] private FloatReference waitTimeForCoroutineRotateAroundPLayer;
    
    private Transform playerTransform;

    [SerializeField] private RotationCenter rotationCenter;

    private Player[] players;

    private bool isAnimationRunning;

    private float currentRotationValue;


    private void OnEnable()
    {
        MapRotator.RotateMap += RotateMap;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Map Rotation Script Loaded");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key down");
            RotateMap();
        }
        
    }
    private void OnDisable()
    {
        MapRotator.RotateMap -= RotateMap;
    }

    private void RotateMap()
    {
        switch (rotationCenter)
        {
            case RotationCenter.Self:
                if (!isAnimationRunning)
                {
                    isAnimationRunning = true;
                    transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + angleToAddForRotation.Value), animationTime.Value).OnComplete(() => { isAnimationRunning = false; });
                }
                break;

            case RotationCenter.Player:

               if (!isAnimationRunning)
               {
                    StartCoroutine(CRotateAroundPlayer());
               }
               break;
        }
        
    }

    private IEnumerator CRotateAroundPlayer()
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
            
            transform.RotateAround(playerTransform.position, new Vector3(0, 0, 1), rotationSpeed.Value * Time.deltaTime);
            
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
                if (transform.eulerAngles.z >= currentRotationValue + angleToAddForRotation.Value)
                {
                    isAnimationRunning = false;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRotationValue + angleToAddForRotation.Value);
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
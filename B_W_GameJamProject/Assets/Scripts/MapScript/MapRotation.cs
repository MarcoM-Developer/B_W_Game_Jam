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

    [SerializeField] private RotationAxis rotationAxis;
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
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
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

                    switch (rotationAxis)
                    {
                        case RotationAxis.X:

                            transform.DORotate(new Vector3(transform.eulerAngles.x + angleToAddForRotation.Value, 0, 0), animationTime.Value).OnComplete(() => { isAnimationRunning = false; });
                            break;

                        case RotationAxis.Y:

                            transform.DORotate(new Vector3(0, transform.eulerAngles.y + angleToAddForRotation.Value, 0), animationTime.Value).OnComplete(() => { isAnimationRunning = false; });
                            break;

                        case RotationAxis.Z:

                            transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + angleToAddForRotation.Value), animationTime.Value).OnComplete(() => { isAnimationRunning = false; });
                            break;
                    }
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

        switch (rotationAxis)
        {
            case RotationAxis.X:

                currentRotationValue = transform.eulerAngles.x;
                break;

            case RotationAxis.Y:

                currentRotationValue = transform.eulerAngles.y;
                break;

            case RotationAxis.Z:

                currentRotationValue = transform.eulerAngles.z;
                break;
        }

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
            switch (rotationAxis)
            {
                case RotationAxis.X:

                    transform.RotateAround(playerTransform.position, new Vector3(1, 0, 0), rotationSpeed.Value * Time.deltaTime);

                    if (transform.eulerAngles.x >= currentRotationValue + angleToAddForRotation.Value)
                    {
                        isAnimationRunning = false;
                        transform.eulerAngles = new Vector3(currentRotationValue + angleToAddForRotation.Value, transform.eulerAngles.y, transform.eulerAngles.z);
                    }
                    break;

                case RotationAxis.Y:

                    transform.RotateAround(playerTransform.position, new Vector3(0, 1, 0), rotationSpeed.Value * Time.deltaTime);

                    if (transform.eulerAngles.y >= currentRotationValue + angleToAddForRotation.Value)
                    {
                        isAnimationRunning = false;
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentRotationValue + angleToAddForRotation.Value, transform.eulerAngles.z);
                    }
                    break;

                case RotationAxis.Z:

                    transform.RotateAround(playerTransform.position, new Vector3(0, 0, 1), rotationSpeed.Value * Time.deltaTime);

                    if (transform.eulerAngles.z >= currentRotationValue + angleToAddForRotation.Value)
                    {
                        isAnimationRunning = false;
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRotationValue + angleToAddForRotation.Value);
                    }
                    break;
            }
            
        }
    }
}


public enum RotationCenter
{
    Player,
    Self
}
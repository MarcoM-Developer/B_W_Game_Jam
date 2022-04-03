using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapRotation : MonoBehaviour
{
    //[SerializeField] private FloatReference byAngle;
    [SerializeField] private FloatReference waitTimeForCoroutineRotateAroundPLayer;
    [SerializeField] private RotationCenter rotationCenter;

    private Player[] players;

    private float animationTime = 20;
    private float rotationSpeed = 100;

    private Transform playerTransform;

    [SerializeField] private BoolReference isAnimationRunning;
    private float currentRotationValue;

    private void OnEnable()
    {
        MapRotator.OnMapRotate += RotateMap;
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
            //RotateMap(byAngle.Value);
        }
        
    }
    private void OnDisable()
    {
      MapRotator.OnMapRotate -= RotateMap;
    }

    private void RotateMap(float byAngle, Transform center)
    {
        switch (rotationCenter)
        {
            case RotationCenter.Self:

                if (!isAnimationRunning.Value)
                {
                    isAnimationRunning.Value = true;
                    transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + byAngle), animationTime).OnComplete(() => { isAnimationRunning.Value = false; });
                }
                break;

            case RotationCenter.Object:

                if (!isAnimationRunning.Value)
                {
                    StartCoroutine(CRotateAroundObject(byAngle, center));
                }
                break;
        }
        
    }

    private IEnumerator CRotateAroundObject(float byAngle, Transform center)
    {
        currentRotationValue = transform.eulerAngles.z;

        isAnimationRunning.Value = true;

        while (isAnimationRunning.Value)
        {
            yield return new WaitForSecondsRealtime(waitTimeForCoroutineRotateAroundPLayer.Value);

            transform.RotateAround(center.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);

            if (currentRotationValue == 270)
            {
                if (transform.eulerAngles.z < 10)
                {
                    isAnimationRunning.Value = false;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                }
            }
            else
            {
                if (transform.eulerAngles.z >= currentRotationValue + byAngle)
                {
                    isAnimationRunning.Value = false;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRotationValue + byAngle);
                }
            }
        }
    }
}


public enum RotationCenter
{
    Player,
    Self,
    Object
}
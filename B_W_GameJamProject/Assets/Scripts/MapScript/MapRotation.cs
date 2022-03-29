using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapRotation : MonoBehaviour
{
    [SerializeField] private FloatReference angleToAddForRotation;
    [SerializeField] private FloatReference animationTime;
    [SerializeField] private RotationAxis rotationAxis;
    private bool isAnimationRunning;

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
    }
}



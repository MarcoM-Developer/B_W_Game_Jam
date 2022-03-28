using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Velocity")]

    [SerializeField] private bool isMoving;
    [SerializeField] private bool isAtMaxSpeed = false;
    [SerializeField] private bool isTimerActive = true;

    [SerializeField] private FloatReference maxSpeed;           //serialize field takes reference from inspector and only if the component is on the same object
    [SerializeField] private FloatReference acceleration;              //otherwise I use getComponent method with GameObject.Find()
    [SerializeField] private FloatReference deceleration;
    [SerializeField] private FloatReference debugTresholdForFalseMaxSpeed;
    
    [SerializeField] private FloatReference remainingTime;
    [SerializeField] private FloatReference currentTime;


    [Header("Components")]

    [SerializeField] private Rigidbody2D playerRigidBody;

    private Vector2 directionVector;

    // Start is called before the first frame update
    private void Start()
    {
        currentTime.Value = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        directionVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        playerRigidBody.velocity = new Vector2(directionVector.x * acceleration.Value, playerRigidBody.velocity.y);

        FlipObjectHorizontally();        
    }
    

    private void FixedUpdate()
    {
       
    }

    private void FlipObjectHorizontally()
    {
        if (directionVector.x < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        }
        else if (directionVector.x > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
        }
    }
}

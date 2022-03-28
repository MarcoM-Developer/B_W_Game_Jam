using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Velocity")]

    [SerializeField] private bool isMoving;
    [SerializeField] private bool isAtMaxSpeed = false;

    [SerializeField] private FloatReference maxSpeed;           //serialize field takes reference from inspector and only if the component is on the same object
    [SerializeField] private FloatReference acceleration;              //otherwise I use getComponent method with GameObject.Find()
    [SerializeField] private FloatReference deceleration;
    [SerializeField] private FloatReference debugTresholdForFalseMaxSpeed;
    
    [SerializeField] private FloatReference remainingTime;
    

    [Header("Components")]

    [SerializeField] private Rigidbody2D playerRigidBody;

    private Vector2 directionVector;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        directionVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

       /*playerRigidBody.velocity = new Vector2(directionVector.x * acceleration.Value, playerRigidBody.velocity.y);
    }*/
        if (directionVector.x > 0 || directionVector.x < 0)
        {
            isMoving = true;
        }
        else if((directionVector.x > 0 && playerRigidBody.velocity.x < 0) || (directionVector.x < 0 && playerRigidBody.velocity.x > 0))
        {
            isMoving = false;
            isAtMaxSpeed = false;
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
        }
        else if (directionVector.x == 0)
        {
            isMoving = false;
            isAtMaxSpeed = false;
        }

        CheckIfMaxSpeedReached();

        FlipGameObjectLeftOrRight();
        
    }

    private void FixedUpdate()
    {
        Move();
        StopMove();
    }

    private void Move()
    {
        if (isMoving && !isAtMaxSpeed)
        {
            playerRigidBody.AddForce(directionVector * acceleration.Value);
        }
    }

    private void StopMove()
    {
        if (!isMoving)
        {
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
        }
    }

    private void CheckIfMaxSpeedReached()
    {
        if (Mathf.Abs(playerRigidBody.velocity.x) >= maxSpeed.Value && !isAtMaxSpeed)
        {
            playerRigidBody.velocity = new Vector2(maxSpeed.Value * directionVector.x, playerRigidBody.velocity.y);
            isAtMaxSpeed = true;
        }

    }

    private void FlipGameObjectLeftOrRight()
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

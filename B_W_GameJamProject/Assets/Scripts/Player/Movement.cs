using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Velocity")]

    [SerializeField] private bool isAtMaxSpeed = false;

    [SerializeField] private FloatReference maxSpeed;           //serialize field takes reference from inspector and only if the component is on the same object
    [SerializeField] private FloatReference acceleration;              //otherwise I use getComponent method with GameObject.Find()
    [SerializeField] private FloatReference deceleration;


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
        DetectInput();
        FlipGameObjectLeftOrRight();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void DetectInput()
    {
        directionVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
    }

    private void Move()
    {
        if ((directionVector.x > 0) || ((directionVector.x < 0)))
        {
            CheckIfMaxSpeedReached();
            if (!isAtMaxSpeed)
            {
                playerRigidBody.AddForce(directionVector * acceleration.Value, ForceMode2D.Force);
            }
        }

        if ((directionVector.x > 0 && playerRigidBody.velocity.x < 0) || (directionVector.x < 0 && playerRigidBody.velocity.x > 0))
        {
            //Debug.Log("you are not at max speed and your input is in other direction compared to your movement");
            CheckIfMaxSpeedReached();
            StopMove();
            if (!isAtMaxSpeed)
            {
                playerRigidBody.AddForce(directionVector * acceleration.Value, ForceMode2D.Force);
            }
        }

        if (directionVector.x == 0)
        {
            isAtMaxSpeed = false;
            StopMove();
        }
    }

    private void StopMove()
    {
         if (Mathf.Abs(playerRigidBody.velocity.x) > 0)
         {
             playerRigidBody.velocity = new Vector2(0,playerRigidBody.velocity.y);
         }
    }

    private void CheckIfMaxSpeedReached()
    {
        if (Mathf.Abs(playerRigidBody.velocity.x) >= maxSpeed.Value && !isAtMaxSpeed)
        {
            playerRigidBody.velocity = new Vector2(maxSpeed.Value * directionVector.x, playerRigidBody.velocity.y);
            isAtMaxSpeed = true;
        }
        else if ((Mathf.Abs(playerRigidBody.velocity.x) >= maxSpeed.Value && isAtMaxSpeed) && ((directionVector.x > 0 && playerRigidBody.velocity.x < 0) || (directionVector.x < 0 && playerRigidBody.velocity.x > 0)) )
        {
            //Debug.Log("you are at max speed and your input is in other direction compared to your movement");
            isAtMaxSpeed = false;
            StopMove();
        }
        else if (Mathf.Abs(playerRigidBody.velocity.x) < maxSpeed.Value)
        {
            isAtMaxSpeed = false;
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

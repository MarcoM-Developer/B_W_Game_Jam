using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Velocity")]

    [SerializeField] private FloatReference maxSpeed;           //serialize field takes reference from inspector and only if the component is on the same object
    [SerializeField] private FloatReference acceleration;              //otherwise I use getComponent method with GameObject.Find()
    [SerializeField] private FloatReference deceleration;
    [SerializeField] private bool isAtMaxSpeed = false;
    [SerializeField] private bool flag = false;

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
        if (directionVector.x < 0)
        {

        }
    }

    private void FixedUpdate()
    {
        PlayerAddForceMovementWithRightPhysics();
        //PlayerVelocityMovement();
    }

    private void PlayerAddForceMovementWithRightPhysics()
    {
        CheckIfPlayerHasReachedMaxSpeed();

        MovePlayerInADirectionIfPressesInputsOrStopItIfPressesNothing();

        ReduceVelocityWhenPlayerIsMovingInADirectionAndSuddenlyChangesIt();

        IfPlayerReachesMaxSpeedOnXStopAddingForceAndSetVelocityOnlyOnce();

    }

    /*private void PlayerVelocityMovement()
    {
        playerRigidBody.velocity = new Vector2(directionVector.x * maxSpeed.Value, playerRigidBody.velocity.y);
    }*/

    private void CheckIfPlayerHasReachedMaxSpeed()
    {
        if (Mathf.Abs(playerRigidBody.velocity.x) >= maxSpeed.Value)
        {
            isAtMaxSpeed = true;
        }
        else
        {
            isAtMaxSpeed = false;
        }
    }

    private void IfPlayerReachesMaxSpeedOnXStopAddingForceAndSetVelocityOnlyOnce()
    {
        if (isAtMaxSpeed && !flag)
        {
            flag = true;
            playerRigidBody.velocity = new Vector2(Mathf.Sign(playerRigidBody.velocity.x) * maxSpeed.Value, playerRigidBody.velocity.y);
        }
        else if (isAtMaxSpeed && directionVector.x == 0)
        {
            flag = false;
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
        }
    }

    private void MovePlayerInADirectionIfPressesInputsOrStopItIfPressesNothing()
    {
        if (directionVector.x != 0)
        {
            playerRigidBody.AddForce(directionVector * acceleration.Value, ForceMode2D.Force);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
        }
    }

    private void ReduceVelocityWhenPlayerIsMovingInADirectionAndSuddenlyChangesIt()
    {
        if (directionVector.x < 0)
        {
            if (playerRigidBody.velocity.x > 0)
            {
                playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
            }
        }
        if (directionVector.x > 0)
        {
            if (playerRigidBody.velocity.x < 0)
            {
                playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
            }
        }
    }

    
}

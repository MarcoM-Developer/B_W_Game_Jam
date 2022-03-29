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

        if ((directionVector.x > 0) || ((directionVector.x < 0)))
        {
            isMoving = true;
            Move();
            CheckIfMaxSpeedReached();
        }
        if ((directionVector.x > 0 && playerRigidBody.velocity.x < 0) || (directionVector.x < 0 && playerRigidBody.velocity.x > 0))
        {
            isMoving = false;
            isAtMaxSpeed = false;
            StopMove();
            //Move();
            //CheckIfMaxSpeedReached();
        }
        else if (directionVector.x == 0)
        {
            isMoving = false;
            isAtMaxSpeed = false;
            StopMove();
        }
        FlipGameObjectLeftOrRight();

    }

    private void Move()
    {
        Debug.Log(2);
        if (isMoving && !isAtMaxSpeed)
        {
            playerRigidBody.AddForce(directionVector * acceleration.Value, ForceMode2D.Force);
            playerRigidBody.drag = 0;
        }
    }

    private void StopMove()
    {
        
        if (Mathf.Abs(playerRigidBody.velocity.x) > 0)
        {
            Debug.Log("1");
            //playerRigidBody.drag = deceleration.Value;
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

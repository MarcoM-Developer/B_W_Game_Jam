using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private FloatReference jumpHeight;
    [SerializeField] private float jumpReducer;
    private bool jump;
    private bool canJump;


    public Rigidbody2D PlayerRigidBody { get => playerRigidBody; set => playerRigidBody = value; }
    public bool CanJump { get => canJump; set => canJump = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump = true;
            }
        }

        if (playerRigidBody.velocity.y > 0 && Input.GetKeyUp(KeyCode.Space))
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.y * jumpReducer);
        }
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            AddVelocityToY();
        }
    }

    private void AddVelocityToY()
    {
        playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0);
        playerRigidBody.AddForce(new Vector2(0, jumpHeight.Value), ForceMode2D.Impulse);
        jump = false;
    }
}

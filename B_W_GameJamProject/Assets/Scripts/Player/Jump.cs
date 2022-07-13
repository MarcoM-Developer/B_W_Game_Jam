using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private FloatReference jumpHeight;
    [SerializeField] private float jumpTimerConstant;
    
    private bool jump;
    private bool canJump;
    private float jumpTimer;

    public Rigidbody2D PlayerRigidBody { get => playerRigidBody; set => playerRigidBody = value; }
    public bool CanJump { get => canJump; set => canJump = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canJump && Input.GetKeyDown(KeyCode.Space))
	{
                jump = true;
		jumpTimer = jumpTimerConstant;
	}
        

	if (jump && Input.GetKeyUp(KeyCode.Space))
	{
		jumpTimer = 0;
		jump = false;
	}

    }

    private void FixedUpdate()
    {
        if (jump)
        {
	    if (jumpTimer == jumpTimerConstant)
	    {
		 playerRigidBody.AddForce(new Vector2(0, 4*jumpHeight.Value), ForceMode2D.Impulse);
	    }
	    else if (jumpTimer > 0)
	    {
	   	 float ratio = (jumpTimer/jumpTimerConstant);
	    
	   	 playerRigidBody.AddForce(new Vector2(0, ratio*jumpHeight.Value), ForceMode2D.Impulse);
	    }
	    jumpTimer -= 1;

        }
    }
}

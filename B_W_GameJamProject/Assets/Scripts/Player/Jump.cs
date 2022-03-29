using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private FloatReference jumpHeight;
    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("JUMPOO");
            jump = true;
            
        }
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            playerRigidBody.AddForce(new Vector2(0,jumpHeight.Value), ForceMode2D.Impulse);
            jump = false;
        }
    }
}

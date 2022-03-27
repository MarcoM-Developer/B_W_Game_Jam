using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private FloatReference speed;              //serialize field takes reference from inspector and only if the component is on the same object
    [SerializeField] private Rigidbody2D playerRigidBody;       //otherwise I use getComponent method with GameObject.Find()
    private Vector2 movementVector;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        playerRigidBody.velocity = new Vector2(movementVector.x * speed.Value * Time.deltaTime , playerRigidBody.velocity.y);
    }
}

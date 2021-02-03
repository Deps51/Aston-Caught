using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed; //Creating a buff for speed
    public Rigidbody rb; //Rigidbody puts the sprite under the control of the physics engine. Sprite is now affected by gravity, collision with other sprites etc etc
    private Vector2 moveDirection; //Vector to hold the new coodrinates of the sprite
    public Camera camera;


    private void Start()
    {
        camera = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
        if (isLocalPlayer)
        {
            //canvas =  GameObject.FindGameObjectWithTag("Canvas");
            //canvas.GetComponent<Canvas>().worldCamera = camera;
            return;
        }

        camera.enabled = false;
        
    }

    // Update is called once per frame
    void Update() //Update effected by the frame rate
    {
        ProcessInputs();
    }

    void FixedUpdate() // Update uneffected by the framerate
    {
        if (hasAuthority)
        {
            Move();
        }
        

    }
    public void ProcessInputs()
    {
        if (isLocalPlayer)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector2(moveX, moveY).normalized;
        }
        
    }
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y  * moveSpeed); 
    }

    public void setSpeed(float speed)
    {
        Debug.Log("new speed of: " + speed);
        moveSpeed = speed;
    }
}

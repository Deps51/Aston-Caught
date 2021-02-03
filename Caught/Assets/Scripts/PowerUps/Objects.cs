using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class Objects : MonoBehaviour
{
    private bool currentlyCaptured = false;
    Rigidbody rb;
    public float movementSpeed = 5f;
    private Vector2 moveDirection;
    public bool isPlayer = false;
    private ControllablePlayer player;

    public void captured()
    {
        //return back as escapee
    }

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(isPlayer)
        {
            /*float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector2(moveX, moveY).normalized;*/
            transform.position = player.gameObject.transform.position;
        }
        else {
            return;
        }

        if (currentlyCaptured)
        {
            return; //stops player being controlled if they're captured
        }
    }

    void FixedUpdate()
    {
        if (isPlayer)
        {
            rb.velocity = new Vector2(moveDirection.x * movementSpeed, moveDirection.y * movementSpeed);
        } else
        {
            rb.velocity = new Vector2(0, 0);
        }

    }


    /*public void btnClick()
    {
        Debug.Log("btn click on morph");
        isPlayer = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        player = collision.gameObject.GetComponent<Escapee>();
    }*/

}
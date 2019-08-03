using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public float borderX;
    public float borderY;
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;
    public Transform graphics;

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        //facing the mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x,mousePos.y - transform.position.y);
        if(mousePos.x>transform.position.x)
        {
            graphics.localScale = new Vector3(0.04f,0.04f,1f);
        }
        else
        {
            graphics.localScale = new Vector3(0.04f,-0.04f,1f);
        }

        transform.up = direction;
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        } 

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        //borders
        if(transform.position.x<-borderX)
        {
            transform.position = new Vector2(-borderX, transform.position.y);
        }
        if(transform.position.x>borderX)
        {
            transform.position = new Vector2(borderX, transform.position.y);
        }
        if(transform.position.y<-borderY)
        {
            transform.position = new Vector2(transform.position.x, -borderY);
        }
        if(transform.position.y>borderY)
        {
            transform.position = new Vector2(transform.position.x, borderY);
        }
    }
}

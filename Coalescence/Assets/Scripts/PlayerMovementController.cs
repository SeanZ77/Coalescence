using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float speed;
    public GameObject sprite;
    public bool input = true;
    public bool facingLeft = false;
    public bool facingRight = false;
    public bool facingUp = false;
    public bool facingDown = true;
    private bool movingLeft = false;
    private bool movingRight = false;
    private bool movingUp = false;
    private bool movingDown = false;
    private Vector2 movePos;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log(facingLeft + " " + facingRight + " " + facingUp + " " + facingDown);
        if (input)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                movingUp = true;
                movingDown = false;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                movingUp = false;
                movingDown = true;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                movingLeft = true;
                movingRight = false;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                movingLeft = false;
                movingRight = true;
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                movingUp = false;
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                movingDown = false;
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                movingLeft = false;
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                movingRight = false;
            }
 

            if (movingUp)
            {
                facingUp = true;
                facingDown = false;
                facingLeft = false;
                facingRight = false;
                movePos.y = 1;
            }

            if (movingDown)
            {
                facingUp = false;
                facingDown = true;
                facingLeft = false;
                facingRight = false;
                movePos.y = -1;
            }

            if (movingLeft)
            {
                facingUp = false;
                facingDown = false;
                facingLeft = true;
                facingRight = false;
                movePos.x = -1;
            }

            if (movingRight)
            {
                facingUp = false;
                facingDown = false;
                facingLeft = false;
                facingRight = true;
                movePos.x = 1;
            }

            if (!movingUp && !movingDown)
            {
                movePos.y = 0;
            }

            if (!movingLeft && !movingRight)
            {
                movePos.x = 0;
            }

            //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.fixedDeltaTime);
        }

        animator.SetBool("walkingUp", movingUp && !movingLeft && !movingRight);
        animator.SetBool("walkingDown", movingDown && !movingLeft && !movingRight);
        animator.SetBool("walkingLeft", movingLeft);
        animator.SetBool("walkingRight", movingRight);
    }

    void FixedUpdate()
    {
        if (input)
        {
            rb.velocity = new Vector2(movePos.x * speed, movePos.y * speed);
        }
    }
}



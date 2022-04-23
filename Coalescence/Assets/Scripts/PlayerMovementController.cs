using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float speed;
    public bool input = true;
    public Vector2 movePos;
    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (input)
        {
            movePos.x = Input.GetAxisRaw("Horizontal");
            movePos.y = Input.GetAxisRaw("Vertical");

            //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.fixedDeltaTime);
        }
    }

    void FixedUpdate()
    {
        if (input)
        {
            rb.velocity = new Vector2(movePos.x * speed, movePos.y * speed);
        }

    }


}



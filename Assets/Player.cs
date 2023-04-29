using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("VARIABLES")]
    public float acceleration;
    public float maxVelocity;

    [Space]
    public float deceleration;

    [Space]
    public float spinSpeed;


    [Space] [Header("REFERENCES")]
    public Rigidbody2D rb;

    Vector2 velocity = Vector2.zero;

    bool isAccelerating = false;
    Vector2 input_Intention = Vector2.zero;
    Vector2 input_Direction = Vector2.zero;

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        if (Input.GetMouseButton(0)) isAccelerating = true;
        else isAccelerating = false;

        input_Direction = transform.up;
        if (isAccelerating)
        {
            input_Intention = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            input_Direction = (input_Intention - (Vector2)transform.position).normalized;
            input_Direction = (input_Direction + (Vector2)transform.up * 5).normalized;
        }
    }

    private void Move()
    {
        // Add acceleration if input is there.
        if (isAccelerating)
        {
            velocity += input_Direction * acceleration * Time.deltaTime;
            if (velocity.magnitude > maxVelocity) velocity = velocity.normalized * maxVelocity;
        }
        // Otherwise, slow down.
        else
        {
            Vector2 prevVelocity = velocity;
            velocity -= velocity.normalized * deceleration * Time.deltaTime;

            if (prevVelocity.x > 0 && velocity.x < 0) velocity.x = 0;
            else if (prevVelocity.x < 0 && velocity.x > 0) velocity.x = 0;

            if (prevVelocity.y > 0 && velocity.y < 0) velocity.y = 0;
            else if (prevVelocity.y < 0 && velocity.y > 0) velocity.y = 0;
        }


        // Move as per velocity.
        //transform.position = (Vector2)transform.position + velocity * Time.deltaTime;
        rb.MovePosition(rb.position + velocity * Time.deltaTime);


        // Rotate to face point of intent.
        Quaternion currentRotation = transform.rotation;
        Quaternion desiredRotation = Quaternion.Euler(0, 0, (180f / Mathf.PI * Mathf.Atan2(input_Intention.y - transform.position.y, input_Intention.x - transform.position.x)) - 90f);

        transform.rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * spinSpeed * velocity.magnitude / maxVelocity);


        // Debug output.
        Debug.Log("Spd: " + velocity.magnitude + "/" + maxVelocity + " MPH");
    }
}

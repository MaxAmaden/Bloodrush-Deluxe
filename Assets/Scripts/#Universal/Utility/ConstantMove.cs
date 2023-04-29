using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMove : MonoBehaviour
{
    public CardinalDirection direction;
    public float speed;
    public float acceleration = 0;

    private void Update()
    {
        speed += acceleration * Time.deltaTime;

        // Move in direction.
        switch (direction)
        {
            case CardinalDirection.up:
                transform.localPosition += transform.up * Time.deltaTime * speed;
                break;
            case CardinalDirection.right:
                transform.localPosition += transform.right * Time.deltaTime * speed;
                break;
            case CardinalDirection.down:
                transform.localPosition -= transform.up * Time.deltaTime * speed;
                break;
            case CardinalDirection.left:
                transform.localPosition -= transform.right * Time.deltaTime * speed;
                break;
        }
    }
}

public enum CardinalDirection { up, right, down, left }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private float maxTimeInArea = 5f;
    [SerializeField] private float slowspeed = 0.5f;

    private float currentTimeInArea = 0f;
    private float movementspeed;

    private Rigidbody2D rb;
    private Vector2 movementDirection;

    private Rect topLeftAreaBounds = new Rect(-16f, 1f, 15f, 8f);
    private Rect topRightAreaBounds = new Rect(1f, 1f, 15f, 8f);
    private Rect bottomLeftAreaBounds = new Rect(-16f, -9f, 15f, 8f);
    private Rect bottomRightAreaBounds = new Rect(1f, -9f, 15f, 8f);

    void Start()
    {
        rb = GetComponent < Rigidbody2D>();
    }

    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (topLeftAreaBounds.Contains(transform.position))
        {
            movementDirection.x = -movementDirection.x;
            movementDirection.y = -movementDirection.y;
        }

        if (topRightAreaBounds.Contains(transform.position))
        {
            currentTimeInArea += Time.deltaTime;
            if (currentTimeInArea >= maxTimeInArea)
            {
                transform.position = new Vector3(0f, 0f, transform.position.z);
                currentTimeInArea = 0f;
            }
        }
        else
        {
            currentTimeInArea = 0f;
        }
    }

    void FixedUpdate()
    {
        bool inBottomLeftArea = bottomLeftAreaBounds.Contains(transform.position);

        if (inBottomLeftArea)
        {
            movementspeed = PlayerAttributes.speed * slowspeed; // Reduce speed in the bottom left area
        }
        else
        {
            movementspeed = PlayerAttributes.speed; // Reset the movement speed to its original value
        }

        rb.velocity = movementDirection.normalized * movementspeed;
    }


}

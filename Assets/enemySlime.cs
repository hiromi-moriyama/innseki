using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySlime : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the slime moves
    private bool moveRight = true; // Determines the initial direction

    private Animator slimeAnimator; // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the GameObject
        slimeAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the slime horizontally
        if (moveRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        // Check if the slime should change direction
        if (transform.position.x > 5f)
        {
            moveRight = false;
        }
        else if (transform.position.x < -5f)
        {
            moveRight = true;
        }

    }
}

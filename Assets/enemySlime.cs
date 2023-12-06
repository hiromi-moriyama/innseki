using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemySlime : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the slime moves
    private bool moveRight = true; // Determines the initial direction
    public string scenename;

    private Animator animator; // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the same GameObject
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the slime horizontally
        if (moveRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            animator.SetFloat("Horizontal", 1);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            animator.SetFloat("Horizontal", 0);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug log to verify item pickup
            Debug.Log("Item picked up!");

            // Destroy the item
            Destroy(gameObject);

            // Load the next scene
            SceneManager.LoadScene(scenename);
        }
    }
}
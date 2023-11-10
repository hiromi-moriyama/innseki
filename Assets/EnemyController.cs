using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private Rigidbody2D rb;
    private Vector2 initialDirection;
    private bool hasMovedToPlayer = false;
    public string scenename;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetTarget();

        if (target)
        {
            initialDirection = (target.position - transform.position).normalized;
        }
    }

    private void Update()
    {
        if (!target)
        {
            GetTarget();
        }
    }

    private void FixedUpdate()
    {
        if (!hasMovedToPlayer)
        {
            MoveTowardsPlayer();
        }
        else
        {
            // Continue moving in the initial direction.
            rb.velocity = initialDirection * speed;
        }
    }

    private void MoveTowardsPlayer()
    {
        if (target)
        {
            Vector2 moveDirection = target.position - transform.position;
            moveDirection.Normalize();
            rb.velocity = moveDirection * speed;

            // Set the flag to indicate that you've moved to the player.
            hasMovedToPlayer = true;
        }
    }

    private void GetTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with Player");
            PlayerAttributes.health -= 50;
            Destroy(gameObject);

            if (PlayerAttributes.health < 1)
            {
                SceneManager.LoadScene(scenename);
            }
        }
    }

    private void OnBecameInvisible()
    {
        // If the enemy goes off-screen, destroy it.
        Destroy(gameObject);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blackhole : MonoBehaviour
{
    public string scenename;
    public float moveSpeed = 5f;  // Adjust the speed as needed

    private Rigidbody2D rb;

    private void Start()
    {
        // Cache the Rigidbody2D component for efficiency
        rb = GetComponent<Rigidbody2D>();

        // Start moving the black hole in a random diagonal direction
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), -1f).normalized;
        rb.velocity = randomDirection * moveSpeed;
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

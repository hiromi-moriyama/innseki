using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] private float chaseSpeed = 3f; // Speed at which the enemy chases the player

    private Transform playerTransform;
    private bool shouldChasePlayer = false;
    public string scenename;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial position of the enemy to (14, 8)
        transform.position = new Vector3(14f, -8f, transform.position.z);

        // Find the player's Transform component
        playerTransform = GameObject.FindWithTag("Player").transform;

        if (playerTransform == null)
        {
            Debug.LogError("Player not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within the specified area
        if (IsPlayerInChaseArea())
        {
            shouldChasePlayer = true;
        }
        else
        {
            shouldChasePlayer = false;
        }

        if (shouldChasePlayer)
        {
            ChasePlayer();
        }
    }

    private bool IsPlayerInChaseArea()
    {
        // Check if the player's position is within the specified area
        return playerTransform.position.x >= 0f && playerTransform.position.x <= 16f &&
               playerTransform.position.y >= -9f && playerTransform.position.y <= 0f;
    }

    private void ChasePlayer()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // Move towards the player's position
        transform.Translate(directionToPlayer * chaseSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                SceneManager.LoadScene(scenename);
        }
    }
}

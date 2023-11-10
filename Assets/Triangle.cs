using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the triangle when the player comes in contact
            TriangleSpawner triangleSpawner = FindObjectOfType<TriangleSpawner>();
            PlayerAttributes.points += 1;
            if (triangleSpawner != null)
            {
                triangleSpawner.SpawnTriangle(); // Spawn a new triangle
            }
        }
    }
}


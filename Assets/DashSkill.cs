using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DashSkill : MonoBehaviour
{
    // Called when the item comes in contact with the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // Debug log to verify item pickup
                Debug.Log("Item picked up!");

                // Destroy the item
                Destroy(gameObject);

                // Load the next scene
                SceneManager.LoadSceneAsync(1);

                // Set CanDash and CanUseBarrier to true in the PlayerController script
                playerController.CanDash = true;
                playerController.CanUseBarrier = true;
            }
        }
    }

}

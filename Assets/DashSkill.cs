using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : MonoBehaviour
{
    public float dashDistance = 2f; // Distance to dash
    public KeyCode dashKey = KeyCode.LeftShift; // Key to trigger the dash

    private bool canDash = false; // Flag to check if the player can dash

    // Update is called once per frame
    void Update()
    {
        // Check if the player can dash (contact with the item) and the dash key is pressed
        if (canDash && Input.GetKeyDown(dashKey))
        {
            // Trigger the dash ability
            Dash();
        }
    }

    // Called when the player enters the trigger zone of the item
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item")) // Assuming the item has the "Item" tag
        {
            canDash = true; // Set the flag to allow dashing
        }
    }

    // Called when the player exits the trigger zone of the item
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            canDash = false; // Reset the flag when leaving the item's trigger zone
        }
    }

    // Dash ability
    private void Dash()
    {
        // Get the input direction
        Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // Apply the dash in the input direction
        transform.Translate(dashDirection * dashDistance);

        // Disable the ability for a short duration (adjust as needed)
        StartCoroutine(DisableDash());
    }

    // Coroutine to disable the dash ability for a short duration
    IEnumerator DisableDash()
    {
        canDash = false;
        yield return new WaitForSeconds(1f); // Adjust the duration as needed
        canDash = true;
    }
}

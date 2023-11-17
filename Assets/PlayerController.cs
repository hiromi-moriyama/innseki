using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    // Dash variables
    public float dashDistance = 2f; // Distance to dash
    public KeyCode dashKey = KeyCode.LeftShift; // Key to trigger the dash
    private bool canDash = true; // Flag to check if the player can dash

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Check if the player can dash and the dash key is pressed
        if (canDash && Input.GetKeyDown(dashKey))
        {
            // Trigger the dash ability
            Dash();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection.normalized * PlayerAttributes.speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player Layer: " + gameObject.layer);
        Debug.Log("Collision Layer: " + collision.collider.gameObject.layer);
        Debug.Log(collision.collider.name);
    }

    // Dash ability
    private void Dash()
    {
        // Apply the dash in the input direction
        transform.Translate(movementDirection.normalized * dashDistance);

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

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    public bool CanDash = true;
    public bool CanUseBarrier = true;

    // Dash variables
    public float dashDistance = 2f;
    public KeyCode dashKey = KeyCode.LeftShift;
    public float dashCooldown = 5f; // Adjust the cooldown duration as needed

    // Barrier variables
    public float barrierRadius = 2f;
    public float barrierOuterRadius = 3f; // Adjust the outer radius
    public KeyCode barrierKey = KeyCode.Tab;
    public float barrierDuration = 5f; // Adjust the barrier duration
    public float barrierCooldown = 5f; // Adjust the cooldown duration as needed
    public LayerMask barrierLayer; // Set this in the inspector to the layers you want the barrier to affect
    public GameObject barrierPrefab; // Drag and drop a circle sprite prefab for the barrier

    // Speed variable
    public float speed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Allow movement in all scenes
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Check if the current scene is scene index 4
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            // Allow skill usage only in scene index 4
            if (CanDash && Input.GetKeyDown(dashKey))
            {
                Dash();
            }

            if (CanUseBarrier && Input.GetKeyDown(barrierKey))
            {
                UseBarrier();
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movementDirection.normalized * speed;
    }

    void Dash()
    {
        // Check if the current scene is scene index 4 before dashing
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            // Calculate the dash direction based on the player's input direction
            Vector3 dashDirection = movementDirection.normalized;

            // Move the player in the dash direction for the dash distance
            transform.Translate(dashDirection * dashDistance);

            // Disable the ability for a short duration (dashCooldown)
            StartCoroutine(DisableDash());
        }
    }

    void UseBarrier()
    {
        // Check if the current scene is scene index 4 before using the barrier
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            // Create the barrier as a child of the player
            GameObject barrier = Instantiate(barrierPrefab, transform.position, Quaternion.identity);
            barrier.transform.parent = transform;

            // Set the layer of the barrier to PlayerIgnoreBarrier
            barrier.layer = LayerMask.NameToLayer("PlayerIgnoreBarrier");

            // Set the scale of the barrier to the outer radius
            barrier.transform.localScale = new Vector3(barrierOuterRadius * 2, barrierOuterRadius * 2, 1);

            // Enable the script to handle collisions and destruction
            BarrierScript barrierScript = barrier.AddComponent<BarrierScript>();
            barrierScript.Initialize(this, barrierDuration);

            // Disable the ability for a short duration (barrierCooldown)
            StartCoroutine(DisableBarrier(barrier));
        }
    }

    IEnumerator DisableDash()
    {
        CanDash = false;
        yield return new WaitForSeconds(dashCooldown);

        // Reset the dash ability after the cooldown
        CanDash = true;
    }

    IEnumerator DisableBarrier(GameObject barrier)
    {
        Debug.Log("Barrier activated, current speed: " + rb.velocity.magnitude);

        yield return new WaitForSeconds(barrierCooldown);

        // Destroy the barrier object after the cooldown
        Destroy(barrier);

        // Disable the ability for a short duration (barrierCooldown)
        CanUseBarrier = false;
        Debug.Log("Barrier cooldown, speed after cooldown: " + rb.velocity.magnitude);

        yield return new WaitForSeconds(barrierCooldown);

        // Reset the barrier ability after the cooldown
        CanUseBarrier = true;
    }

}

public class BarrierScript : MonoBehaviour
{
    private PlayerController playerController;
    private float barrierDuration;

    public void Initialize(PlayerController playerController, float duration)
    {
        this.playerController = playerController;
        barrierDuration = duration;
        StartCoroutine(DisableBarrier());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is on a layer that the barrier should affect
        if (playerController.barrierLayer == (playerController.barrierLayer | (1 << other.gameObject.layer)) &&
            other.gameObject.layer != LayerMask.NameToLayer("PlayerIgnoreBarrier"))
        {
            Destroy(other.gameObject);
        }
    }

    IEnumerator DisableBarrier()
    {
        yield return new WaitForSeconds(barrierDuration);

        // Destroy the barrier object after the duration
        Destroy(gameObject);
    }
}

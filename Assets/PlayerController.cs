using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private Animator animator;

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
    private Camera _camera;

    // Speed variable
    public float speed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _camera = Camera.main;
    }

    void Update()
    {
        // Allow movement in all scenes
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        {
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
        PreventPlayerOffScreen();
    }

    void Dash()
    {
            // Calculate the dash direction based on the player's input direction
            Vector3 dashDirection = movementDirection.normalized;

            // Move the player in the dash direction for the dash distance
            transform.Translate(dashDirection * dashDistance);

            // Disable the ability for a short duration (dashCooldown)
            StartCoroutine(DisableDash());
    }

    void UseBarrier()
    {
            // Create the barrier as a child of the player
            Vector3 barrierOffset = new Vector3(0f, 1.5f, 0f);

            // Create the barrier at the player's position with the offset
            GameObject barrier = Instantiate(barrierPrefab, transform.position + barrierOffset, Quaternion.identity);
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

    private void PreventPlayerOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
        if((screenPosition.x < 0 && rb.velocity.x < 0) || (screenPosition.x > _camera.pixelWidth && rb.velocity.x > 0))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if ((screenPosition.y < 0 && rb.velocity.y < 0) || (screenPosition.y > _camera.pixelWidth && rb.velocity.y > 0))
        {
            rb.velocity = new Vector2(rb.velocity.y, 0);
        }
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
        // Check if the colliding object is on the default layer (layer 0) and not the player
        if (other.gameObject.layer == 0 && !other.CompareTag("Player"))
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

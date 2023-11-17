using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 movementDirection;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent < Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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

}

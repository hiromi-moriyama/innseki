using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private Animator animator;

    public float speed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        //animator.SetFloat("Horizontal", movementDirection.x);
        //animator.SetFloat("Vertical", movementDirection.y);
    }

    void FixedUpdate()
    {
        rb.velocity = movementDirection.normalized * speed;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagMovement : MonoBehaviour
{
    public float MovementSpeed;
    public Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInputs();
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(moveX) > 0) moveY = 0;

        movement = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = movement * MovementSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    private Rigidbody2D body;
    private Vector2 axisMovement;
    private Animator animator;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get joystick input using the new Input System
        axisMovement = Gamepad.current != null ? Gamepad.current.leftStick.ReadValue() : Vector2.zero;

        // Update the animator parameter
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        body.velocity = axisMovement.normalized * speed;
        CheckForFlipping();
    }

    private void CheckForFlipping()
    {
        bool movingLeft = axisMovement.x < 0;
        bool movingRight = axisMovement.x > 0;

        if (movingLeft)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }

        if (movingRight)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
    }

    private void UpdateAnimator()
    {
        // Update the "isMoving" parameter in the animator
        bool isMoving = axisMovement.magnitude > 0; // Check if the player is moving
        animator.SetBool("isMoving", isMoving);
    }
}

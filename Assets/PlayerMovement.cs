using Photon.Pun;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView photonView;
    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded, isJumping;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;

    [Header("JumpSetting Settings")]
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float jumpMultiplier = 2f;
    [SerializeField] float jumpTime;
    float jumpCounter;

    private float swipeThreshold = 50f;

    private Vector2 startTouchPosition, endTouchPosition;
    private Vector2 vectorGravity;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!photonView.IsMine)
        {
            rb.isKinematic = true;
        }
        vectorGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            DetectSwipeInput();
            FallFaster();
        }
    }

    private void DetectSwipeInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    HandleSwipe();
                    break;
            }
        }
        else
        {
            isJumping = false;
        }
    }

    private void HandleSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;
        swipeDelta = JumpController(swipeDelta);
        MovementController(swipeDelta);
    }

    private void MovementController(Vector2 swipeDelta)
    {
        if (Mathf.Abs(swipeDelta.x) > swipeThreshold || Mathf.Abs(swipeDelta.y) > swipeThreshold)
        {
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }
            }
        }
    }

    private void FallFaster()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity -= vectorGravity * fallMultiplier * Time.deltaTime;
        }

        if (rb.velocity.y > 0 && isJumping)
        {
            rb.velocity += vectorGravity * jumpMultiplier * Time.deltaTime;
            
            jumpCounter += Time.deltaTime;
            if(jumpCounter >= jumpTime)
            {
                isJumping = false;
            }
        }
    }
    private Vector2 JumpController(Vector2 swipeDelta)
    {
        if (swipeDelta.magnitude < swipeThreshold)
        {
            if (isGrounded)
            {
                Jump();
            }
        }
        return swipeDelta;
    }

    private void MoveRight()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    private void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpCounter = 0;
        isGrounded = false;
        isJumping = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

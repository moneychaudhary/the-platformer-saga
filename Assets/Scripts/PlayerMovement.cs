using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jump = 8f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask jumpablePlatform;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;

    private Animator animator;

    public bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        Health.playerHealth = 1f;
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       float x = Input.GetAxisRaw("Horizontal");
        if (x == 0)
        {
            animator.SetBool("Walk", false);
        }
        else
        {
            animator.SetBool("Walk", true);
        }
       rigidBody.velocity = new Vector2(x * speed, rigidBody.velocity.y);
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && IsGrounded() && canJump) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jump);
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    private bool IsGrounded() {

        return Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.2f,
            jumpableGround
        ) || Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.2f,
            jumpablePlatform
        );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float speed = 5f;
    private float jump = 7f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       float x = Input.GetAxisRaw("Horizontal");
       rigidBody.velocity = new Vector2(x * speed, rigidBody.velocity.y);

        if (Input.GetButtonDown("Jump")) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jump);
        }
    }
}

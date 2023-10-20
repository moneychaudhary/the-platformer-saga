using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHit : MonoBehaviour
{
    public PlayerHealthBar playerHealthBar;
    [SerializeField] public LayerMask platform;
    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            Destroy(collision.gameObject);

            boxCollider = GetComponent<BoxCollider2D>();
            RaycastHit2D hit = Physics2D.BoxCast(
               boxCollider.bounds.center,
               boxCollider.bounds.size,
               0f,
               Vector2.down,
               0.2f,
               platform
            );
            if (hit) {
                Color platformColor = hit.transform.GetComponent<Renderer>().material.color;
                if(Math.Round(platformColor.r, 2) == 1.0 && Math.Round(platformColor.g, 2) == 0.65 && Math.Round(platformColor.b, 2) == 0 && Math.Round(platformColor.a, 2) == 0.0) {
                    playerHealthBar.Damage(0.1f);
                    return;
                }
            }
            playerHealthBar.Damage(0.05f);
        }
    }
}

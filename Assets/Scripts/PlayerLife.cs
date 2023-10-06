using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Trap")) {
            gameObject.SetActive(false);
            GameOver();
        }
    }

    private void GameOver() {
           SceneManager.LoadScene("GameOver");
    }
}

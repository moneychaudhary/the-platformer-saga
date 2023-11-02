using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialLevelOne : MonoBehaviour
{
    private Color bulletColor;
    private BoxCollider2D boxCollider;
    public GameObject arrow;
    public GameObject enemy;
    public GameObject aimAndShoot;
    [SerializeField] public LayerMask platform;
    public Text instructionText;

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    public void shoot()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.2f,
            platform);
        if (hit)
        {
            bulletColor = hit.transform.GetComponent<Renderer>().material.color;
            if (bulletColor!= null && Math.Round(bulletColor.r, 2) == 1.0 && Math.Round(bulletColor.g, 2) == 0.34 && Math.Round(bulletColor.b, 2) == 0.2 && Math.Round(bulletColor.a, 2) == 1.0)
            {
                Debug.Log(bulletColor);
                arrow.SetActive(false);
                enemy.SetActive(true);
                aimAndShoot.SetActive(true);
                instructionText.text = "Shoot the enemy by hopping to enemy colored platform.";
                //SceneManager.LoadScene("Tutorisl-Level2");
            }
        }
    }
}

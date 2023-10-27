using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToggleVisibility : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    [SerializeField] private BoxCollider2D collide;
    [SerializeField] private bool isVisible = true;
    [SerializeField] private float disappearDelay = 3f;
    [SerializeField] private float reappearDelay = 2f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        collide = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!isVisible)
        {
            reappearDelay -= Time.deltaTime;
            if (reappearDelay <= 0)
            {
                ToggleVisibilityState();
                reappearDelay = 2f;
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            isVisible = !isVisible;
            rend.enabled = isVisible;
            collide.enabled = isVisible;
        }
    }

    void ToggleVisibilityState()
    {
        isVisible = !isVisible;
        rend.enabled = isVisible;
        collide.enabled = isVisible;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartDisappearTimer());
            // Debug.Log("Player hitting platform");
            if(Math.Round(rend.material.color.r, 2) == 1.0 && Math.Round(rend.material.color.g, 2) == 0 && Math.Round(rend.material.color.b, 2) == 0 && Math.Round(rend.material.color.a, 2) == 0) {
                if(Health.playerHealth < 1.0f)
                {
                    Health.playerHealth += 0.2f;
                    collision.transform.GetComponent<PlayerAimHand>().bulletCount = Math.Max(0, collision.transform.GetComponent<PlayerAimHand>().bulletCount - 3);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StopAllCoroutines();
        disappearDelay = 3f;
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Player not hitting platform");
        }
    }

    IEnumerator StartDisappearTimer()
    {
        while (disappearDelay > 0)
        {
            yield return null;
            disappearDelay -= Time.deltaTime;
        }

        ToggleVisibilityState();
    }
}

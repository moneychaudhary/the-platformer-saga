using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToggleVisibility : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    [SerializeField] private BoxCollider2D collide;
    [SerializeField] private bool isVisible = true;
    [SerializeField] private float disappearDelay = 10f;
    [SerializeField] private float reappearDelay = 2f;
    [SerializeField] private float fadingDelay = 7f;
    [SerializeField] private float timeToStartFading = 3f;

    private Color originalColor;
    private float currentAlpha = 1f; // Current alpha value

    void Start()
    {
        rend = GetComponent<Renderer>();
        collide = GetComponent<BoxCollider2D>();
        originalColor = rend.material.color;
    }

    void Update()
    {
        if (fadingDelay <= 0)
        {
            timeToStartFading -= Time.deltaTime;

            if (timeToStartFading <= 3f && timeToStartFading > 2f)
            {
                currentAlpha = 0.75f;
            }
            else if (timeToStartFading <= 2f && timeToStartFading > 1f)
            {
                currentAlpha = 0.5f;
            }
            else if (timeToStartFading <= 1f && timeToStartFading > 0f)
            {
                currentAlpha = 0.25f;
            }
            else if (timeToStartFading <= 0f)
            {
                currentAlpha = 0f;
            }

            Color newColor = rend.material.color;
            newColor.a = currentAlpha;
            rend.material.color = newColor;
        }
        if (!isVisible)
        {
            reappearDelay -= Time.deltaTime;
            if (reappearDelay <= 0)
            {
                ToggleVisibilityState();
                reappearDelay = 2f;
                fadingDelay = 7f;
                timeToStartFading = 3f;
                rend.material.color = originalColor;
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
            Debug.Log("Player hitting platform");
            if (Math.Round(rend.material.color.r, 2) == 1.0 && Math.Round(rend.material.color.g, 2) == 0 && Math.Round(rend.material.color.b, 2) == 0 && Math.Round(rend.material.color.a, 2) == 0)
            {
                if (Health.playerHealth < 1.0f)
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
        disappearDelay = 10f;
        fadingDelay = 7f;
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player not hitting platform");
        }
    }

    IEnumerator StartDisappearTimer()
    {
        while (disappearDelay > 0)
        {
            yield return null;
            disappearDelay -= Time.deltaTime;
            fadingDelay -= Time.deltaTime;
        }

        ToggleVisibilityState();
    }
}

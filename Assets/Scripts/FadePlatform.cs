using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FadePlatform : MonoBehaviour
{
    public SpriteRenderer objectRenderer;
    public float fadeDuration = 2.0f; // Duration of the fade in seconds

    private Color originalColor;
    private float currentAlpha = 1.0f; // Start with full opacity

    void Start()
    {
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
            StartCoroutine(FadeSprite());
        }
        else
        {
            Debug.LogWarning("Sprite Renderer not assigned!");
        }
    }

    IEnumerator FadeSprite()
    {
        float timer = 0.0f;
        Color newColor = objectRenderer.material.color;

        while (timer < fadeDuration)
        {
            currentAlpha = Mathf.Lerp(1.0f, 0.0f, timer / fadeDuration);
            newColor.a = currentAlpha;
            objectRenderer.material.color = newColor;
            timer += Time.deltaTime;
            yield return null;
        }

        newColor.a = 0.0f; // Ensure it's fully faded
        objectRenderer.material.color = newColor;
    }
}

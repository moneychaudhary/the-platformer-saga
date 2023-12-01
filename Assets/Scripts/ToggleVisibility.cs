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
    [SerializeField] private float platformDespawnTime = 10f;
    [SerializeField] private float platformRespawnTime = 2f;
    [SerializeField] private float timer2;

    public ColorChanger colorChangerInstance;
    public List<ColorChanger.ColorData> colorsList;
    private int colorIndex;
    public SpriteRenderer objectRenderer;
    public float fadeDuration = 2.0f;

    private Color originalColor;
    private float currentAlpha = 1.0f;

    void Start()
    {
        colorChangerInstance = GetComponent<ColorChanger>();
        colorsList = colorChangerInstance.colors;
        colorIndex = colorChangerInstance.currentColorIndex;
        rend = GetComponent<Renderer>();
        collide = GetComponent<BoxCollider2D>();

        disappearDelay = platformDespawnTime;
        reappearDelay = platformRespawnTime;
        //StartCoroutine(TrackColorChanges());
    }

    void Update()
    {
        colorsList = colorChangerInstance.colors;
        colorIndex = colorChangerInstance.currentColorIndex;

        if (!isVisible)
        {
            reappearDelay -= Time.deltaTime;
            if (reappearDelay <= 0)
            {
                ToggleVisibilityState();
                //reappearDelay = 2f;
                reappearDelay = platformRespawnTime;
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
            originalColor = objectRenderer.material.color;
            StartCoroutine(StartDisappearTimer());
            //Debug.Log("Landed on color:" + transform.GetComponent<Renderer>().material.color);
            //StartCoroutine(FadeSprite());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StopAllCoroutines();
        //disappearDelay = 3f;
        disappearDelay = platformDespawnTime;
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

            if (disappearDelay >= fadeDuration)
            {
                StartCoroutine(FadeSprite());
            }
        }

        GameObject.Find("Analytics").GetComponent<GoogleFormUploader>().dissapearCount++;

        Debug.Log("Platform disappear count: " + GameObject.Find("Analytics").GetComponent<GoogleFormUploader>().dissapearCount);

        ToggleVisibilityState();
        //objectRenderer.material.color = originalColor;
        objectRenderer.material.color = colorsList[colorIndex].color;
    }

    IEnumerator FadeSprite()
    {
        float timer = 0.0f;
        
        Color newColor = objectRenderer.material.color;

        while (timer < fadeDuration)
        {
            timer2 = timer;
            currentAlpha = Mathf.Lerp(1.0f, 0.0f, timer / fadeDuration);
            newColor.a = currentAlpha;
            objectRenderer.material.color = newColor;
            timer += Time.deltaTime;
            yield return null;
        }

        newColor.a = 0.0f; // Ensure it's fully faded
        //objectRenderer.material.color = newColor;
        //objectRenderer.material.color = colorsList[colorIndex].color;
    }
    /*
    IEnumerator TrackColorChanges()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f); // Check color changes every 1 second (adjust as needed)

            Color currentColor = objectRenderer.material.color;

            // Check if the color has changed
            if (currentColor != originalColor)
            {
                // Color has changed, do something with the new color
                Debug.Log("Material color has changed: " + currentColor);

                // Update the previous color to the new color
                originalColor = currentColor;
            }
        }
    }
    */
}

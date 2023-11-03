using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ToggleVisibility : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    [SerializeField] private BoxCollider2D collide;
    [SerializeField] private SpriteShapeRenderer spriteRend;
    [SerializeField] private bool isVisible = true;
    [SerializeField] private float disappearDelay = 10f;
    [SerializeField] private float reappearDelay = 2f;
    [SerializeField] private float fadingDelay = 7f;
    [SerializeField] private float fadingTime = 3f;
    [SerializeField] private Color originalColor;  // Store the original color
    [SerializeField] private Color targetColor = Color.black;  // Color to transition to
    [SerializeField] private float lerpSpeed = 0.5f;
    [SerializeField] private Color currentColor;

    //private Color currentColor; // Track the current color
    private bool isFading = false;     // Track if the object is in the fading state

    void Start()
    {
        rend = GetComponent<Renderer>();
        spriteRend = GetComponent<SpriteShapeRenderer>();
        collide = GetComponent<BoxCollider2D>();
        //originalColor = spriteRend.material.color; // Store the original color
        //currentColor = originalColor; // Set the current color to the original color
        InvokeRepeating("GetMaterialColor", 0f, 15f);
    }

    void Update()
    {
     //   Debug.Log(originalColor);
        if (!isVisible)
        {
            reappearDelay -= Time.deltaTime;
            if (reappearDelay <= 0)
            {
                ToggleVisibilityState();
                if (isFading)
                {
                    // If the object was fading, set the color back to the original color
                    ChangeColor(currentColor, 2f);
                }
                reappearDelay = 2f;
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            isVisible = !isVisible;
            rend.enabled = isVisible;
            spriteRend.enabled = isVisible;
            collide.enabled = isVisible;
        }
    }

    void GetMaterialColor()
    {
        currentColor = rend.material.color;
        Debug.Log("Material color: " + currentColor);
    }

    void ChangeColor(Color newColor, float speed)
    {
        spriteRend.material.color = Color.Lerp(spriteRend.material.color, newColor, Time.deltaTime * speed);
    }

    void ToggleVisibilityState()
    {
        isVisible = !isVisible;
        rend.enabled = isVisible;
        spriteRend.enabled = isVisible;
        collide.enabled = isVisible;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //currentColor = spriteRend.material.color; // Store the current color
            StartCoroutine(StartDisappearTimer());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StopAllCoroutines();
        disappearDelay = 10f;
        fadingDelay = 7f;
        fadingTime = 3f;
        isFading = false;
        // Set the color back to the stored current color
        ChangeColor(currentColor, 2f);
    }

    IEnumerator StartDisappearTimer()
    {
        while (disappearDelay > 0)
        {
            yield return null;
            disappearDelay -= Time.deltaTime;
            fadingDelay -= Time.deltaTime;
            if (fadingDelay <= 0)
            {
                fadingTime -= Time.deltaTime;
                if (fadingTime <= 3f)
                {
                    isFading = true;
                    // Change the color to targetColor (black) during the fading period
                    ChangeColor(targetColor, lerpSpeed);
                }
            }
        }

        GameObject.Find("Analytics").GetComponent<GoogleFormUploader>().dissapearCount++;

        Debug.Log("Platform disappear count: " + GameObject.Find("Analytics").GetComponent<GoogleFormUploader>().dissapearCount);

        ToggleVisibilityState();
    }
}

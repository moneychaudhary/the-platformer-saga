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
            //Debug.Log("Landed on color:" + transform.GetComponent<Renderer>().material.color);
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

        GameObject.Find("Analytics").GetComponent<GoogleFormUploader>().dissapearCount++;

        Debug.Log("Platform disappear count: " + GameObject.Find("Analytics").GetComponent<GoogleFormUploader>().dissapearCount);

        ToggleVisibilityState();
    }
}

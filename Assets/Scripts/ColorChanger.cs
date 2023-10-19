using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Material[] materials;
    private Renderer platformRenderer;
    private int currentMaterialIndex;
    private float timeRemaining;
    public float materialChangeInterval = 15f;

    public TextMesh timerText; // Reference to the TextMesh component for displaying the timer.
    public GameObject nextMaterialIndicator; // Reference to the GameObject representing the next material indicator.
    private int nextMaterialIndex;

    private void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        currentMaterialIndex = Random.Range(0, materials.Length);
        platformRenderer.material = materials[currentMaterialIndex];
        timeRemaining = materialChangeInterval;
        UpdateTimerText();
        UpdateNextMaterialIndicator();

        StartCoroutine(ChangeMaterial());
    }

    private IEnumerator ChangeMaterial()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
            UpdateTimerText();

            if (timeRemaining <= 0f)
            {
                currentMaterialIndex = nextMaterialIndex;
                platformRenderer.material = materials[currentMaterialIndex];
                timeRemaining = materialChangeInterval;
                UpdateNextMaterialIndicator();
            }
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = timeRemaining.ToString("F1") + "s";
        }
    }

    private void UpdateNextMaterialIndicator()
    {
        if (nextMaterialIndicator != null)
        {
            nextMaterialIndex = Random.Range(0,materials.Length);
            nextMaterialIndicator.GetComponent<Renderer>().material = materials[nextMaterialIndex];
            //Debug.Log(nextMaterialIndicator.GetComponent<Renderer>().material.name);
            // You can set the position and other properties of the nextMaterialIndicator here.
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.U2D;

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


    public Transform floatTransform;
    public float floatHeight = 1.0f;       // The height of the float movement.
    public float floatSpeed = 1.0f;        // The speed of the float movement.

    private Vector3 initialPosition;       // Store the initial position of the object.

    private void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        currentMaterialIndex = Random.Range(0, materials.Length);
        platformRenderer.material.color = materials[currentMaterialIndex].color;
        timeRemaining = materialChangeInterval;
        UpdateTimerText();
        UpdateNextMaterialIndicator();

        StartCoroutine(ChangeMaterial());

        // Store the initial position of the object.
        initialPosition = floatTransform.position;

        // Start the float coroutine.
        StartCoroutine(FloatObject());
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
        //if (nextMaterialIndicator != null)
        {
            nextMaterialIndex = Random.Range(0,materials.Length);
            transform.Find("NextColor/Arrow/Triangle").GetComponent<Renderer>().material.color = materials[nextMaterialIndex].color;
            transform.Find("NextColor/Arrow/Square").GetComponent<Renderer>().material.color = materials[nextMaterialIndex].color;
            //nextMaterialIndicator.GetComponent<Renderer>().material = materials[nextMaterialIndex];
            //Debug.Log(nextMaterialIndex);
            // You can set the position and other properties of the nextMaterialIndicator here.
        }
    }

    IEnumerator FloatObject()
    {
        while (true)
        {
            // Calculate the target position for floating up.
            Vector3 targetUpPosition = initialPosition + Vector3.up * floatHeight;

            // Move the object up.
            while (floatTransform.position != targetUpPosition)
            {
                floatTransform.position = Vector3.MoveTowards(floatTransform.position, targetUpPosition, floatSpeed * Time.deltaTime);
                yield return null;
            }

            // Calculate the target position for floating down.
            Vector3 targetDownPosition = initialPosition;

            // Move the object down.
            while (floatTransform.position != targetDownPosition)
            {
                floatTransform.position = Vector3.MoveTowards(transform.position, targetDownPosition, floatSpeed * Time.deltaTime);
                yield return null;
            }

            // Repeat the loop.
        }
    }
}

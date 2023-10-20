using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.U2D;

public class ColorChanger : MonoBehaviour
{
    public List<ColorData> colors = new List<ColorData>(); // List of color data.
    private SpriteShapeRenderer platformRenderer;
    private int currentColorIndex;
    private float timeRemaining;
    public float colorChangeInterval = 15f;

    public TextMesh timerText; // Reference to the TextMesh component for displaying the timer.
    public GameObject nextColorIndicator; // Reference to the GameObject representing the next color indicator.
    private int nextColorIndex;

    public Transform floatTransform;
    public float floatHeight = 1.0f; // The height of the float movement.
    public float floatSpeed = 1.0f; // The speed of the float movement.

    private Vector3 initialPosition; // Store the initial position of the object.

    [System.Serializable]
    public class ColorData
    {
        public string colorName;
        public Color color;
    }

    private void Start()
    {
        platformRenderer = GetComponent<SpriteShapeRenderer>();
        currentColorIndex = Random.Range(0, colors.Count);
        platformRenderer.material.color = colors[currentColorIndex].color;
        timeRemaining = colorChangeInterval;
        UpdateTimerText();
        UpdateNextColorIndicator();

        StartCoroutine(ChangeColor());

        // Store the initial position of the object.
        initialPosition = floatTransform.position;

        // Start the float coroutine.
        StartCoroutine(FloatObject());
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
            UpdateTimerText();

            if (timeRemaining <= 0f)
            {
                currentColorIndex = nextColorIndex;
                platformRenderer.material.color = colors[currentColorIndex].color;
                timeRemaining = colorChangeInterval;
                UpdateNextColorIndicator();
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

    private void UpdateNextColorIndicator()
    {
        if (nextColorIndicator != null)
        {
            nextColorIndex = Random.Range(0, colors.Count);
            //nextColorIndicator.GetComponent<Renderer>().material.color = colors[nextColorIndex].color;
            nextColorIndicator.transform.Find("Square").GetComponent<Renderer>().material.color = colors[nextColorIndex].color;
            nextColorIndicator.transform.Find("Triangle").GetComponent<Renderer>().material.color = colors[nextColorIndex].color;
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
        }
    }
}

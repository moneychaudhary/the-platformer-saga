using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.Linq;

public class ColorChanger : MonoBehaviour
{
    public enum PlatType
    {
        Regular,
        Boost,
        Special
    }

    public List<ColorData> colors = new List<ColorData>();
    private Renderer platformRenderer;
    public int currentColorIndex;
    private float timeRemaining;
    public float colorChangeInterval = 15f;

    // Define the frequencies for each PlatType.
    public float regularFrequency = 0.7f; // Very frequent
    public float boostFrequency = 0.2f;  // Less frequent
    public float specialFrequency = 0.1f; // Very rare

    public TextMesh timerText;
    public GameObject nextColorIndicator;
    private int nextColorIndex;

    public Transform floatTransform;
    public float floatHeight = 1.0f;
    public float floatSpeed = 1.0f;
    private Vector3 initialPosition;

    [System.Serializable]
    public class ColorData
    {
        public string colorName;
        public Color color;
        public PlatType platType;
    }

    private void Start()
    {
        floatTransform.gameObject.SetActive(false);
        platformRenderer = GetComponent<Renderer>();

        // Initialize the platform color based on the selected PlatType.
        InitializePlatformColor();

        timeRemaining = colorChangeInterval;
        UpdateTimerText();
        UpdateNextColorIndicator();

        StartCoroutine(ChangeColor());

        // Store the initial position of the object.
        initialPosition = floatTransform.position;

        // Start the float coroutine.
        StartCoroutine(FloatObject());
    }

    private void InitializePlatformColor()
    {
        // Calculate the total frequency.
        float totalFrequency = regularFrequency + boostFrequency + specialFrequency;

        // Generate a random value to determine the PlatType.
        float randomValue = Random.value * totalFrequency;

        // Determine the PlatType based on the random value.
        if (randomValue < regularFrequency)
        {
            currentColorIndex = GetRandomColorIndex(PlatType.Regular);
        }
        else if (randomValue < regularFrequency + boostFrequency)
        {
            currentColorIndex = GetRandomColorIndex(PlatType.Boost);
        }
        else
        {
            currentColorIndex = GetRandomColorIndex(PlatType.Special);
        }

        // Set the platform color.
        platformRenderer.material.color = colors[currentColorIndex].color;
    }

    private int GetRandomColorIndex(PlatType platType)
    {
        // Get a list of color indices that match the specified PlatType.
        List<int> matchingIndices = colors.FindAll(data => data.platType == platType)
                                           .Select(data => colors.IndexOf(data))
                                           .ToList();

        if (matchingIndices.Count == 0)
        {
            // No colors with the specified PlatType were found; fall back to a random color.
            return Random.Range(0, colors.Count);
        }

        // Randomly select an index from the matching indices.
        int randomIndex = matchingIndices[Random.Range(0, matchingIndices.Count)];

        return randomIndex;
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
            UpdateTimerText();

            if (timeRemaining <= 3f) floatTransform.gameObject.SetActive(true);

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
            floatTransform.gameObject.SetActive(false);
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

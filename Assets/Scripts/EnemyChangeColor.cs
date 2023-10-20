using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChangeColor : MonoBehaviour
{
    public List<ColorData> colors = new List<ColorData>();
    private Renderer enemyRenderer;
    private int currentColorIndex;
    private float timeRemaining;
    public float colorChangeInterval = 15f;
    public Transform platformColors;

    [System.Serializable]
    public class ColorData
    {
        public string colorName;
        public Color color;
    }

    private void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        currentColorIndex = Random.Range(0, colors.Count);
        enemyRenderer.material.color = colors[currentColorIndex].color;
        timeRemaining = colorChangeInterval;
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;

            if (timeRemaining <= 0f)
            {
                currentColorIndex = Random.Range(0,colors.Count);
                enemyRenderer.material.color = colors[currentColorIndex].color;
                timeRemaining = colorChangeInterval;
            }
        }
    }
}

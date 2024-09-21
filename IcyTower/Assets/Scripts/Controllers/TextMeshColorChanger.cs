using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Changes the color of a TextMeshProUGUI component over time.
/// </summary>
public class TextMeshColorChanger : MonoBehaviour
{
    private const float colorStep = 1 / 255f; // Step value for color changes.

    [SerializeField] private bool running = true; // Controls whether the color change is active.
    [SerializeField] private Color[] colors = new Color[4]; // Array of colors to cycle through.
    [SerializeField] private TextMeshProUGUI textMesh;

    private eTargetColor[] targets = new eTargetColor[4] { eTargetColor.BlueFromPurple, eTargetColor.Purple, 
        eTargetColor.Purple, eTargetColor.BlueFromCyan }; // Color transition targets.

    private void Start()
    {
        StartCoroutine(changeColors()); 
    }

    private IEnumerator changeColors()
    {
        while (running)
        {
            textMesh.colorGradient = new VertexGradient(changeColor(0), changeColor(1), changeColor(2), changeColor(3)); // Update text color gradient.
            yield return new WaitForSecondsRealtime(1 / 255f); // Wait before the next color change.
        }
    }

    private Color changeColor(int index)
    {
        Color color = colors[index];

        switch (targets[index])
        {
            case eTargetColor.Purple:
                color.r += colorStep; // Adjust red channel.
                if (color.r >= 1) targets[index] = eTargetColor.BlueFromPurple; // Change target if limit reached.
                break;
            case eTargetColor.BlueFromPurple:
                color.r -= colorStep; // Adjust red channel.
                if (color.r <= 0) targets[index] = eTargetColor.Cyan; 
                break;
            case eTargetColor.Cyan:
                color.g += colorStep; // Adjust green channel.
                if (color.g >= 1) targets[index] = eTargetColor.BlueFromCyan; 
                break;
            case eTargetColor.BlueFromCyan:
                color.g -= colorStep; // Adjust green channel.
                if (color.g <= 0) targets[index] = eTargetColor.Purple;
                break;
        }

        colors[index] = color; // Update color array.
        return color; 
    }

    private enum eTargetColor
    {
        Purple,
        BlueFromPurple,
        Cyan,
        BlueFromCyan
    }
}

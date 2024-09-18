using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshColorChanger : MonoBehaviour
{
    private const float colorStep = 1 / 255f;

    [SerializeField] private bool running = true;
    [SerializeField] private Color[] colors = new Color[4];
    [SerializeField] private TextMeshProUGUI textMesh;

    private eTargetColor[] targets = new eTargetColor[4] { eTargetColor.BlueFromPurple, eTargetColor.Purple, eTargetColor.Purple, eTargetColor.BlueFromCyan };

    private void Start()
    {
        StartCoroutine(changeColors());   
    }

    private IEnumerator changeColors()
    {
        while (running)
        {
            textMesh.colorGradient = new VertexGradient(changeColor(0), changeColor(1), changeColor(2), changeColor(3));
            yield return new WaitForSeconds(1 / 255f);
        }
    }

    private Color changeColor(int index)
    {
        Color color = colors[index];

        switch (targets[index])
        {
            case eTargetColor.Purple:

                color.r += colorStep;

                if(color.r >= 1)
                {
                    targets[index] = eTargetColor.BlueFromPurple;
                }
                break;
            case eTargetColor.BlueFromPurple:

                color.r -= colorStep;

                if (color.r <= 0)
                {
                    targets[index] = eTargetColor.Cyan;
                }
                break;
            case eTargetColor.Cyan:

                color.g += colorStep;

                if (color.g >= 1)
                {
                    targets[index] = eTargetColor.BlueFromCyan;
                }
                break;
            case eTargetColor.BlueFromCyan:

                color.g -= colorStep;

                if (color.g <= 0)
                {
                    targets[index] = eTargetColor.Purple;
                }
                break;
        }


        colors[index] = color;
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

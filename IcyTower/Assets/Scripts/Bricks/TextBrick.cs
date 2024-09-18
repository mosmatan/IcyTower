using TMPro;
using UnityEngine;

public class TextBrick : MonoBehaviour
{
    [SerializeField] private int change;
    [SerializeField] private int value;
    [SerializeField] private Brick brick;
    [SerializeField] private TextMeshPro textMesh;

    private void Start()
    {
        brick.PositionChanged += Brick_PositionChanged;
    }

    private void Brick_PositionChanged()
    {
        value += change;
        textMesh.text = value.ToString();
    }
}

using TMPro;
using UnityEngine;

public class TextBrick : MonoBehaviour
{
    [SerializeField] private int change; // Value change on position update.
    [SerializeField] private int value; // Current displayed value.
    [SerializeField] private Brick brick; // Reference to the associated Brick object.
    [SerializeField] private TextMeshPro textMesh; // Text display component.
    
    private int bigBrick;

    private void Start()
    {
        bigBrick = GameManager.Instance.FloorsForLevel; // Initialize bigBrick.
        brick.PositionChanged += Brick_PositionChanged; // Subscribe to position changes.
        brick.AddPositionChangedTime();
    }

    private void Brick_PositionChanged(Brick sender, int times)
    {
        value += change; 
        textMesh.text = value.ToString(); // Update displayed text.

        // Adjust the brick's position and scale based on the value.
        if (value % bigBrick == 0)
        {
            sender.transform.position = new Vector3(0, sender.transform.position.y, sender.transform.position.z);
            sender.transform.localScale = new Vector3(13, sender.transform.localScale.y, sender.transform.localScale.z);
            transform.localScale = new Vector3(0.2f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            sender.transform.localScale = new Vector3(3, sender.transform.localScale.y, sender.transform.localScale.z);
            transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.z);
        }
    }
}
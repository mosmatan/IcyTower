using TMPro;
using UnityEngine;

public class TextBrick : MonoBehaviour
{
    [SerializeField] private int change;
    [SerializeField] private int value;
    [SerializeField] private Brick brick;
    [SerializeField] private TextMeshPro textMesh;
    
    private int bigBrick;

    private void Start()
    {
        bigBrick = GameManager.Instance.FloorsForLevel;
        brick.PositionChanged += Brick_PositionChanged;
        brick.AddPostionChangedTime();
    }

    private void Brick_PositionChanged(Brick sender, int times)
    {
        value += change;
        textMesh.text = value.ToString();

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

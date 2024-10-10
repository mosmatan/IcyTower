using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents a boost object that applies an effect when the player collides with it.
/// </summary>
public class Boost : MonoBehaviour
{
    [SerializeField] public UnityEvent ApplyBoost = new UnityEvent(); // Event invoked when the boost is applied.
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float speed; // Speed at which the boost moves downwards.

    private void Start()
    {
        // Set the initial velocity of the boost to move downwards.
        rigidbody.velocity = Vector2.down * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player.
        if (other.transform.CompareTag("Player"))
        {
            ApplyBoost.Invoke(); 
            gameObject.SetActive(false); // Deactivate the boost object after applying.
        }
    }

    private void OnEnable()
    {
        // Reset the velocity of the boost when it is enabled.
        rigidbody.velocity = Vector2.down * speed;
    }
}
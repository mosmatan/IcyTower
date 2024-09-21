using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a star object that can be thrown with force and has a limited lifetime.
/// </summary>
public class Star : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody; 
    [SerializeField] private float force = 1f; // Force applied when throwing the star.
    [SerializeField] private float lifeTime = 1f; // Lifetime before the star deactivates.
    [SerializeField] private List<Sprite> sprites = new List<Sprite>(); // List of sprites for the star.
    [SerializeField] private SpriteRenderer spriteRenderer; 

    private void Awake()
    {
        selectSprite(); // Randomly select a sprite when the star is created.
    }

    private void OnEnable()
    {
        throwStar(); 
        StartCoroutine(startLifeTime()); // Start the coroutine for managing the star's lifetime.
    }

    private void selectSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }

    private void throwStar()
    {
        float forceDirection = Random.Range(-1f, 1f); // Randomize the throw direction.
        rigidbody.AddForce(new Vector2(force * forceDirection, 0), ForceMode2D.Impulse); 
    }

    private IEnumerator startLifeTime()
    {
        yield return new WaitForSeconds(lifeTime); // Wait for the star's lifetime to expire.
        yield return new WaitForSeconds(0.01f); // Additional wait to ensure cleanup.
        gameObject.SetActive(false); // Deactivate the star.
    }
}
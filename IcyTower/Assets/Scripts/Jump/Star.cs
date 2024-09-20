using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float force = 1f;
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        selectSprite();
    }

    private void OnEnable()
    {
        throwStar();
        StartCoroutine(startLifeTime());
    }

    private void selectSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }

    private void throwStar()
    {
        float forceFirection = Random.Range(-1f, 1f);

        rigidbody.AddForce(new Vector2(force * forceFirection, 0), ForceMode2D.Impulse);
    }

    private IEnumerator startLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);

        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }
}

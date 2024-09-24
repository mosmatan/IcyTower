using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Boost : MonoBehaviour
{
    [SerializeField] public UnityEvent ApplyBoost = new UnityEvent();
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float speed;

    private void Start()
    {
        rigidbody.velocity = Vector2.down * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            ApplyBoost.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        rigidbody.velocity = Vector2.down * speed;
    }
}

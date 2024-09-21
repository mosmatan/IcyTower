using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using System;

/// <summary>
/// Handles the fading in and out of a UI screen.
/// </summary>
public class FadeScreen : IFadeScreen
{
    public override event Action Faded; // Triggered when fading completes.
    public override event Action Fading; // Triggered when fading starts.

    [SerializeField] private Image image; 
    [SerializeField] private float seconds = 1; // Duration of the fade.
    [SerializeField] private Color color; 

    private bool isFading = false; // Tracks if a fade is currently in progress.
    private float stepsToChange; 

    public override float Seconds => seconds; // Gets the duration.

    private void Start()
    {
        image.color = color; 
        stepsToChange = 1 / (255f * seconds); // Calculate change step.
        FadeOut(); 
    }

    public override void FadeOut()
    {
        if (!isFading)
        {
            StartCoroutine(fadeOut()); // Start fade out coroutine.
            OnFading(); // Notify that fading has started.
        }
    }

    private IEnumerator fadeOut()
    {
        while (image.color.a > 0)
        {
            image.color = new Color(color.r, color.g, color.b, image.color.a - stepsToChange);
            yield return new WaitForSeconds(seconds / 255f); // Wait for next step.
        }

        OnFaded(); 
        gameObject.SetActive(false); // Deactivate the game object.
    }

    public override void FadeIn()
    {
        if (!isFading)
        {
            OnFading(); // Notify that fading has started.
            StartCoroutine(fadeIn()); // Start fade in coroutine.
        }
    }

    private IEnumerator fadeIn()
    {
        while (image.color.a < 1)
        {
            image.color = new Color(color.r, color.g, color.b, image.color.a + stepsToChange);
            yield return new WaitForSeconds(seconds / 255f); // Wait for next step.
        }

        OnFaded(); 
    }

    protected override void OnFaded()
    {
        isFading = false; 
        Faded?.Invoke(); // Trigger Faded event.
    }

    protected override void OnFading()
    {
        isFading = true; 
        Fading?.Invoke(); // Trigger Fading event.
    }
}

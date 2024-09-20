using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using System;

public class FadeScreen : IFadeScreen
{
    public override event Action Faded;
    public override event Action Fading;

    [SerializeField] private Image image;
    [SerializeField] private float seconds = 1;
    [SerializeField] Color color;

    private bool isFading = false;
    private float stepsToChange;
    public override float Seconds => seconds;

    private void Start()
    {
        image.color = color;
        stepsToChange = 1 / (255f * seconds);
        FadeOut();
    }

    public override void FadeOut()
    {
        if(!isFading)
        {
            StartCoroutine(fadeOut());
            OnFading();
        }
    }

    private IEnumerator fadeOut()
    {
        while (image.color.a > 0)
        {
            image.color =new Color(color.r, color.g, color.b, image.color.a - stepsToChange);

            yield return new WaitForSeconds(seconds / 255f);
        }

        OnFaded();
        gameObject.SetActive(false);
    }

    public override void FadeIn()
    {
        if (!isFading)
        {
            OnFading();
            StartCoroutine(fadeIn());
        }
    }

    private IEnumerator fadeIn()
    {
        while (image.color.a < 1)
        {
            image.color = new Color(color.r, color.g, color.b, image.color.a + stepsToChange);

            yield return new WaitForSeconds(seconds / 255f);
        }

        OnFaded();
    }

    protected override void OnFaded()
    {
        isFading = false;

        if (Faded != null)
        {
            Faded();
        }
    }

    protected override void OnFading()
    {
        isFading = true;

        if (Fading != null)
        {
            Fading();
        }
    }
}

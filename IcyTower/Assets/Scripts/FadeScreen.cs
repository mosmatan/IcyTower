using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts;

public class FadeScreen : IFadeScreen
{
    private const float stepsToChange = 1 / 255f;

    [SerializeField] private Image image;
    [SerializeField] private int seconds = 1;
    [SerializeField] Color color;

    public override int Seconds => seconds;

    private void Awake()
    {
        image.color = color;
        FadeOut();
    }

    public override void FadeOut()
    {
        StartCoroutine(fadeOut());
    }

    private IEnumerator fadeOut()
    {
        while (image.color.a > 0)
        {
            image.color =new Color(color.r, color.g, color.b, image.color.a - stepsToChange);

            yield return new WaitForSeconds(seconds / 255f);
        }
    }

    public override void FadeIn()
    {
        StartCoroutine(fadeIn());
    }

    private IEnumerator fadeIn()
    {
        while (image.color.a < 1)
        {
            image.color = new Color(color.r, color.g, color.b, image.color.a + stepsToChange);

            yield return new WaitForSeconds(seconds / 255f);
        }
    }
}

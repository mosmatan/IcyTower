using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class StartGameItem : SelectableItem
    {
        [SerializeField] private float timeOffset = 0.5f;
        [SerializeField] private IFadeScreen fadeScreen;

        public override void Select()
        {
            StartCoroutine(changeScene(fadeScreen.Seconds));
        }

        private IEnumerator changeScene(float seconds)
        {
            fadeScreen.FadeIn();
            yield return new WaitForSeconds(seconds + timeOffset);
            SceneManager.LoadScene("GameScene");
        }
    }
}

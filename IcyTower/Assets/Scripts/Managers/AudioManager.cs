using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource scenePlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fadeInAudio(scenePlayer));
    }

    private IEnumerator fadeInAudio(AudioSource source)
    {
        source.volume = 0;
        while (source.volume < 1)
        {
            source.volume += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    private IEnumerator fadeOutAudio(AudioSource source)
    {
        while (source.volume > 0)
        {
            source.volume -= 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void sceneFadeOutAudio()
    {
        StartCoroutine(fadeOutAudio(scenePlayer));
    }
}

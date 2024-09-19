using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource scenePlayer;

    public float Volume { get; set; } = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AudioVolumeChanged += setVolume;
        Volume = GameManager.Instance.AudioVolume;
        StartCoroutine(fadeInAudio(scenePlayer));
    }

    private void OnDestroy()
    {
        GameManager.Instance.AudioVolumeChanged -= setVolume;
    }

    private IEnumerator fadeInAudio(AudioSource source)
    {
        source.volume = 0;

        while (source.volume < Volume)
        {
            source.volume += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }

        source.volume = Volume;
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

    private void setVolume(float volume)
    {
        scenePlayer.volume = volume;
    }
}

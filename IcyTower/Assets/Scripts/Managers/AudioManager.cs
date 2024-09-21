using System.Collections;
using UnityEngine;

/// <summary>
/// Manages audio playback and volume for the game.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource scenePlayer; // Audio source for playing sound in the scene.

    public float Volume { get; set; } = 1f; // Current volume level.
    
    private void Start()
    {
        GameManager.Instance.AudioVolumeChanged += setVolume; 
        Volume = GameManager.Instance.AudioVolume; 
        StartCoroutine(fadeInAudio(scenePlayer)); // Fade in audio at the start.
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AudioVolumeChanged -= setVolume; // Unsubscribe from volume change event.
        }
    }

    private IEnumerator fadeInAudio(AudioSource source)
    {
        source.volume = 0; 

        while (source.volume < Volume)
        {
            source.volume += 0.2f; // Increment volume.
            yield return new WaitForSeconds(0.2f); 
        }

        source.volume = Volume; // Ensure final volume is set.
    }

    private IEnumerator fadeOutAudio(AudioSource source)
    {
        while (source.volume > 0)
        {
            source.volume -= 0.2f; // Decrement volume.
            yield return new WaitForSeconds(0.2f); 
        }
    }

    public void sceneFadeOutAudio()
    {
        StartCoroutine(fadeOutAudio(scenePlayer)); 
    }

    private void setVolume(float volume)
    {
        scenePlayer.volume = volume; // Set audio source volume.
    }
}
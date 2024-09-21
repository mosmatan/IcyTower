using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Manages audio playback for the player, specifically jump sounds.
/// </summary>
public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource playerAudio; // Audio source for playing sounds.
    [SerializeField] private List<AudioClip> jumpsClips; // List of audio clips for jump sounds.
    [SerializeField] private IJumper jumper; // Reference to the jumper interface.

    private void Start()
    {
        jumper.Jumped += playJumpAudio; 
        GameManager.Instance.AudioVolumeChanged += setVolume; 
        playerAudio.volume = GameManager.Instance.AudioVolume; // Set initial volume.
    }

    private void OnDestroy()
    {
        jumper.Jumped -= playJumpAudio; // Unsubscribe from the jump event.
        GameManager.Instance.AudioVolumeChanged -= setVolume; // Unsubscribe from volume change event.
    }

    private void playJumpAudio()
    {
        int index = Random.Range(0, jumpsClips.Count); // Select a random jump sound.
        playerAudio.clip = jumpsClips[index]; // Set the audio clip.
        playerAudio.Play(); // Play the selected sound.
    }

    private void setVolume(float volume)
    {
        playerAudio.volume = volume; // Update the audio volume.
    }
}
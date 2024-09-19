using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private List<AudioClip> jumpsClips;
    [SerializeField] private IJumper jumper;

    private void Start()
    {
        jumper.Jumped += playJumpAudio;
        GameManager.Instance.AudioVolumeChanged += setVolume;
        playerAudio.volume = GameManager.Instance.AudioVolume;
    }
    private void OnDestroy()
    {
        jumper.Jumped -= playJumpAudio;
        GameManager.Instance.AudioVolumeChanged -= setVolume;
    }

    public void playJumpAudio()
    {
        int index = Random.Range(0, jumpsClips.Count);
        playerAudio.clip = jumpsClips[index];
        playerAudio.Play();
    }

    private void setVolume(float volume)
    {
        playerAudio.volume = volume;
    }


}

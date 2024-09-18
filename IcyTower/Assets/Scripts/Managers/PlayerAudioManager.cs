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
    }

    public void playJumpAudio()
    {
        int index = Random.Range(0, jumpsClips.Count);
        playerAudio.clip = jumpsClips[index];
        playerAudio.Play();
    }
}

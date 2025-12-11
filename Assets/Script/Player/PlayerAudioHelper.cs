using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioHelper : MonoBehaviour
{
    public Player player;
    public List<AudioClip> audioClips;
    public List<AudioSource> audioSources;

    public AudioSource jumpSFX;

    private int _index = 0;

    private void Awake()
    {
        if (player == null)
            player = GameObject.FindObjectOfType<Player>();
    }

    public void PlayRandom()
    {
        if(player.PlayerGroundedCheck())
        {
            if(_index >= audioSources.Count) _index = 0;

            var audioSource = audioSources[_index];

            audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
            audioSource.Play();
            _index++;
        }

    }

    public void PlayJumpSFX()
    {
        jumpSFX.Play();
    }
}

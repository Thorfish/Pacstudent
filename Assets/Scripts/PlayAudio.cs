using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip introMusic;
    [SerializeField]
    private AudioClip bgNormalMusic;

    private AudioSource player;

    void Start()
    {
        player=GetComponent<AudioSource>();
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        player.clip = introMusic;
        player.Play();
        yield return new WaitForSeconds(player.clip.length);
        player.clip = bgNormalMusic;
        player.Play();
        player.loop = true;
    }
}

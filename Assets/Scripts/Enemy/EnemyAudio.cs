using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AudioSource screamAudio;
    public AudioSource friendlyAudio;

    public AudioClip yell;

    public void PlayScream()
    {
        screamAudio.pitch = Random.Range(0.8f, 1.2f);
        screamAudio.PlayOneShot(yell, 0.25f);
        screamAudio.Play();
    }
    public void PlayFriendly()
    {
        friendlyAudio.Play();
    }
}

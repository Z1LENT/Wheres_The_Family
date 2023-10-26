using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AudioSource screamAudio;
    public AudioSource friendlyAudio;
   
    public void PlayScream()
    {
        screamAudio.Play();
    }
    public void PlayFriendly()
    {
        friendlyAudio.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMainMenu : MonoBehaviour
{


    public AudioSource ad;
    public AudioSource zombieAd;
    public AudioClip lightning;
    public AudioClip zombie;

    public void PlayLightning()
    {
        ad.PlayOneShot(lightning, 0.5f);
    }
    public void PlayZombie()
    {
        zombieAd.PlayOneShot(zombie, 0.5f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMainMenu : MonoBehaviour
{


    public AudioSource ad;
    public AudioClip lightning;

    public void PlayLightning()
    {
        ad.PlayOneShot(lightning, 0.5f);
    }


}

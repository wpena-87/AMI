using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    static AudioSource musicSound;
    public static bool decreaseMusicVolume;

    void Awake()
    {
        decreaseMusicVolume = false;
        musicSound = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (decreaseMusicVolume && musicSound.volume > 0)
        {
            musicSound.volume -= 0.25f * Time.deltaTime;
        }
    }


}

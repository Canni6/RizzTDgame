using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioDeclined;
    // Start is called before the first frame update
    void Start()
    {
        audioDeclined.clip = (AudioClip)Resources.Load("Sounds/audio_declined");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(AudioSource audioSource) {
        audioSource.Play();
    }
}

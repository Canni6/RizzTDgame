using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioDeclined;
    public AudioSource audioDeathBio;
    public AudioSource audioDeathMech;
    public AudioSource audioShotCannon;
    public AudioSource audioShotRapid;
    public AudioSource audioButtonBlip;
    public AudioSource audioBuild;
    // Start is called before the first frame update
    void Start()
    {
        audioDeclined.clip = (AudioClip)Resources.Load("Sounds/audio_declined");
        audioDeathBio.clip = (AudioClip)Resources.Load("Sounds/audio_death_bio");
        audioDeathMech.clip = (AudioClip)Resources.Load("Sounds/audio_death_mech");
        audioShotCannon.clip = (AudioClip)Resources.Load("Sounds/audio_shot_cannon");
        audioShotRapid.clip = (AudioClip)Resources.Load("Sounds/audio_shot_rapid");
        audioButtonBlip.clip = (AudioClip)Resources.Load("Sounds/audio_button_blip");
        audioBuild.clip = (AudioClip)Resources.Load("Sounds/audio_build");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(AudioSource audioSource) {
        audioSource.Play();
    }
}

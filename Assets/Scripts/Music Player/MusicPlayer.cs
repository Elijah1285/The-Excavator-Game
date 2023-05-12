using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public float music_timer = 10.0f;
    public int track_selection = 1;

    public AudioSource music_1;
    public AudioSource music_2;
    public AudioSource music_3;
    public AudioSource zoom_sound;
    public AudioSource dirt_destroyed_sound;

    void Update()
    {
        if (!music_1.isPlaying && !music_2.isPlaying && !music_3.isPlaying)
        {
            music_timer -= Time.deltaTime;
        }

        if (music_timer <= 0.0f)
        {
            playMusic();
        }
    }

    void playMusic()
    {
        if (track_selection == 1)
        {
            music_1.Play();
        }
        else if (track_selection == 2)
        {
            music_2.Play();
        }
        else if (track_selection == 3)
        {
            music_3.Play();
        }

        track_selection += 1;
        
        if (track_selection > 3)
        {
            track_selection = 1;
        }

        music_timer = 30.0f;
    }

    public void playZoomSound()
    {
        zoom_sound.Play();
    }

    public void playDirtDestroyedSound()
    {
        dirt_destroyed_sound.Play();
    }
}

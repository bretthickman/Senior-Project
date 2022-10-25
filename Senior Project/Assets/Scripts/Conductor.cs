using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Conductor : MonoBehaviour
{
    // Constants
    public const int SPOTS_PER_BEAT = 4; // Sixteenth Notes
    public const int BEATS_PER_BAR = 4;  // 4/4 Time

    AudioSource audioSource;

    public float bpm;
    public float crotchet;
    public float spotLength;

    public double songPosition;
    double nextSpotTime;
    public float offset;

    public int spotNumber;
    public int beatNumber;
    public int barNumber;

    private double tempSongPosition;

    // Start is called before the first frame update
    void Start()
    { 
        bpm = 60;
        nextSpotTime = 0.0;
        offset = 0.07f;
        spotNumber = 0;
        beatNumber = 0;
        barNumber = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        tempSongPosition = (double)AudioSettings.dspTime * audioSource.pitch;
        crotchet = 60 / bpm;
        spotLength = crotchet / SPOTS_PER_BEAT;
        songPosition = 0.0f;
    }

    public void Update()
    {
        double newSongPosition = AudioSettings.dspTime * audioSource.pitch - offset - tempSongPosition;
                                                    // NOTE(alex): - dsptimesong) might be needed
                                                    // "Every frame that I play the song, I record the dspTime at that moment."

        nextSpotTime += newSongPosition - songPosition;
        songPosition = newSongPosition;

        // Updates Bar, Beat, Spot based on Song Position.
        if(nextSpotTime > spotLength)
        {
            ++spotNumber;
            nextSpotTime = nextSpotTime - spotLength;
            if(spotNumber == SPOTS_PER_BEAT)
            {
                spotNumber = 0;
                ++beatNumber;
                if(beatNumber == BEATS_PER_BAR)
                {
                    beatNumber = 0;
                    ++barNumber;
                }
            }
        }
    }
}

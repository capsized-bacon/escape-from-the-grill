using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}

// REFERENCES

/*“Unity Audio: How to make a UI volume slider(the right way) - YouTube”. [Online].
Available: https://www.youtube.com/watch?v=xNHSGMKtlv4.
[Accessed: 30-Jun.-2020].*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioPause : MonoBehaviour
{
    public BackgroundAudioPause pauseThisAudio;
    // Start is called before the first frame update
    void Start()
    {
        BackgroundAudio.Instance.gameObject.GetComponent<AudioSource>().Pause();
    }
}

// REFERENCES

/*“Audio or Music to Continue Playing Between Scene Changes...”. [Online].
Available: https://www.youtube.com/watch?v=82Mn8v55nr0.
[Accessed: 30-Jun.-2020].*/

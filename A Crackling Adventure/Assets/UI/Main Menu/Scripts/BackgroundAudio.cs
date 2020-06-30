using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Play Global
    private static BackgroundAudio instance = null;

    public static BackgroundAudio Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    //Play Global End

    // Update is called once per frame
    void Update()
    {

    }
}

// REFERENCES

/*“Audio or Music to Continue Playing Between Scene Changes...”. [Online].
Available: https://www.youtube.com/watch?v=82Mn8v55nr0.
[Accessed: 30-Jun.-2020].*/

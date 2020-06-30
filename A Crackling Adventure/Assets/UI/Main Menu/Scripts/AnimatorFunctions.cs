using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
    [SerializeField] public MenuButtonController menuButtonController;
    public bool disableOnce;

    void PlaySound(AudioClip whichSound)
    {
        if (!disableOnce)
        {
            menuButtonController.audioSource.PlayOneShot(whichSound);
        }
        else
        {
            disableOnce = false;
        }
    }
}

// REFERENCES

/*“Make A Gorgeous Start Menu(Unity UI Tutorial)! - YouTube”. [Online].
Available: https://www.youtube.com/watch?v=vqZjZ6yv1lA.
	[Accessed: 29-Jun.-2020].*/

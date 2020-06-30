using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{

    // Use this for initialization
    public int index;
    [SerializeField] public bool keyDown;
    [SerializeField] public int maxIndex;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (index < maxIndex)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        index = maxIndex;
                    }
                }
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
    }

}

// REFERENCES

/*“Make A Gorgeous Start Menu(Unity UI Tutorial)! - YouTube”. [Online].
Available: https://www.youtube.com/watch?v=vqZjZ6yv1lA.
	[Accessed: 29-Jun.-2020].*/

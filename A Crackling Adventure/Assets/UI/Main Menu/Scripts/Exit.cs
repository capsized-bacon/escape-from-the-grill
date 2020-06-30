using UnityEngine.UI;
using UnityEngine;
using System.Diagnostics;

public class Exit : MonoBehaviour
{
    public void quit()
    {
        UnityEngine.Debug.Log("Quit has been done.");
        Application.Quit();
    }
}

// REFERENCES

/*“[Unity3D] [Tutorial] How to Create a Quit Button in Unity - YouTube”. [Online].
 * Available: https://www.youtube.com/watch?v=4-X1FDylROA.
 * [Accessed: 29-Jun.-2020].*/

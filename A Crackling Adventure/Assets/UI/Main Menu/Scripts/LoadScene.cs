using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void SceneLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

}

// REFERENCES

/*“Switching Between Scenes in Unity 3D using UI Button - YouTube”. [Online].
Available: https://www.youtube.com/watch?v=FSt5xrFHaFU.
[Accessed: 28-Jun.-2020].*/

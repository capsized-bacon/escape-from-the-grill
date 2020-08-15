using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("GameOver");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
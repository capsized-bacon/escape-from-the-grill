using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


// Placed on ScoreBar digit text as script component
public class ScoreBar : MonoBehaviour
{

    // public ScoreBar scoreBar;
    public TextMeshProUGUI scoreBar;

    void Start()
    {
        scoreBar = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreBar.SetText("{0}", Player.GetInstance().GetScore());
    }

    // Update is called once per frame
    void Update()
    {
        // textPro.SetText("{0}", Player.GetInstance().GetScore());
        scoreBar.SetText("{0}", Player.GetInstance().GetScore());
    }
}
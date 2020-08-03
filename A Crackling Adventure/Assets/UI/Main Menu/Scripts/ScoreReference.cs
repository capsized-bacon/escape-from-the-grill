using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreReference : MonoBehaviour
{
    private TestPlayer testPlayer;
    private TextMeshProUGUI textPro;

/*    void Start()
    {
        // Accesses variables from TestPlayer.cs script.
        GameObject player = GameObject.Find("Player");
        if (player == null)
        {
            UnityEngine.Debug.Log("Failed to find an object called 'Player'");
            this.enabled = false;
            return;
        }
    }*/

    void Update()
    {
        // Accesses text box for Text Mesh Pro.
        textPro = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        textPro.SetText("{0}", Player.GetInstance().GetScore());
        // UnityEngine.Debug.Log("The currentScore is:" + TestPlayer.GetInstance().GetScore());
    }
}
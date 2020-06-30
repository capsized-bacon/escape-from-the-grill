using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreAdd : MonoBehaviour
{
    private TestPlayer testPlayer;
    private TextMeshProUGUI textPro;
    private TextMeshProUGUI textGame;
    private int newScore;
    private int maxScore;

    void Start()
    {
        // Gets the button onClick event from the ScoreAdd button.
        GameObject.Find("ScoreAdd").GetComponent<Button>().onClick.AddListener(TaskOnClick);

        // Sets the ScoreText text to maxScore from TestPlayer.cs.
        /*UnityEngine.Debug.Log("Start().");
        UnityEngine.Debug.Log("maxScore = " + maxScore);
        maxScore = TestPlayer.GetInstance().GetMaxScore();
        UnityEngine.Debug.Log("maxScore = " + maxScore);*/
        textPro = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        // textPro.SetText("{0}", TestPlayer.GetInstance().GetMaxScore());
        UnityEngine.Debug.Log("Next line: " + TestPlayer.GetInstance().GetMaxScore());
        // Accesses text box for Text Mesh Pro.
        textGame = GameObject.Find("GameText").GetComponent<TextMeshProUGUI>();
    }

    // Task to do when button is clicked.
    void TaskOnClick()
    {
        GivePoints(20);
        textPro.SetText("{0}", TestPlayer.GetInstance().GetScore()); // [1]
        textGame.SetText("Yummy! :P");
        if (TestPlayer.GetInstance().GetScore() >= 1000)
        {
            textGame.SetText("Weirdo. You broke the numbers!");
        }
    }

    void GivePoints(int points)
    {
        newScore = TestPlayer.GetInstance().GetScore();
        TestPlayer.GetInstance().SetScore(newScore += points);
    }
}

// REFERENCES

/*[1] “Referencing non static variables from another script? C# - Unity ...”. [Online].
Available: https://answers.unity.com/questions/42843/referencing-non-static-variables-from-another-scri.html.
[Accessed: 30-Jun.-2020].*/

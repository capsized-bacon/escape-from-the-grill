using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSubtract : MonoBehaviour
{
    private TestPlayer testPlayer;
    private TextMeshProUGUI textPro;
    private TextMeshProUGUI textGame;
    private int newScore;

    void Start()
    {
        // Gets the button onClick event from the ScoreSubtract button.
        GameObject.Find("ScoreSubtract").GetComponent<Button>().onClick.AddListener(TaskOnClick);

        // Sets the ScoreText text to maxScore from TestPlayer.cs.
        textPro = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        textPro.SetText("{0}", TestPlayer.GetInstance().GetMaxScore());
        // Accesses text box for Text Mesh Pro.
        textGame = GameObject.Find("GameText").GetComponent<TextMeshProUGUI>();
    }

    // Task to do when button is clicked.
    void TaskOnClick()
    {
        if (TestPlayer.GetInstance().GetScore() > 0)
        {
            TakePoints(20);
            textPro.SetText("{0}", TestPlayer.GetInstance().GetScore());
            textGame.SetText("My points! :(");
        }
        if (TestPlayer.GetInstance().GetScore() == 0)
        {
            textGame.SetText("Centrelink, I await thee. :X");
        }
    }

    void TakePoints(int points)
    {
        // Calculate new score value.
        newScore = TestPlayer.GetInstance().GetScore();
        TestPlayer.GetInstance().SetScore(newScore -= points);
    }
}

// REFERENCES

/*[1] “Referencing non static variables from another script? C# - Unity ...”. [Online].
Available: https://answers.unity.com/questions/42843/referencing-non-static-variables-from-another-scri.html.
[Accessed: 30-Jun.-2020].*/

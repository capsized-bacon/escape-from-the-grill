using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthSubtract : MonoBehaviour
{
    private TestPlayer testPlayer;
    private TextMeshProUGUI textGame;
    private int newHealth;

    void Start()
    {
        // Gets the button onClick event from the HealthSubtract button.
        GameObject.Find("HealthSubtract").GetComponent<Button>().onClick.AddListener(TaskOnClick);

        // Sets the GameText text to various things.
        textGame = GameObject.Find("GameText").GetComponent<TextMeshProUGUI>();
    }
    // Task to do when button is clicked.
    void TaskOnClick()
    {
        if (TestPlayer.GetInstance().GetHealth() > 0) // [1]
        {
            TakeDamage(20);
            textGame.SetText("Please, no. :O");
        }
        if (TestPlayer.GetInstance().GetHealth() == 0)
        {
            GameOver();
        }
    }

    void TakeDamage(int damage)
    {
        // Calculate new health value.
        newHealth = TestPlayer.GetInstance().GetHealth(); // [2]
        TestPlayer.GetInstance().SetHealth(newHealth -= damage);
    }
    // textGame.SetText("Score: " + GameStatus.GetInstance().GetScore() + " Lives: " + GameStatus.GetInstance().GetLives();
    void GameOver()
    {
        textGame.SetText("I am dead. :X");
        FindObjectOfType<GameManager>().GameOver();
    }
}

// REFERENCES

/*[1] “Unity Tutorial: Preserving Data between Scene Loading/Switching...”. [Online].
Available: https://www.youtube.com/watch?v=WchH-JCwVI8.
[Accessed: 30-Jun.-2020].*/

/*[2] “GAME OVER - How to make a Video Game in Unity(E08) - YouTube”. [Online].
Available: https://www.youtube.com/watch?v=VbZ9_C4-Qbo.
[Accessed: 30-Jun.-2020].*/

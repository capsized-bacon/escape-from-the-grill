using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthAdd : MonoBehaviour
{
    private TestPlayer testPlayer;
    private TextMeshProUGUI textGame;
    private int newHealth;

    void Start()
    {
        // Gets the button onClick event from the HealthAdd button.
        GameObject.Find("HealthAdd").GetComponent<Button>().onClick.AddListener(TaskOnClick);

        // Accesses text box for Text Mesh Pro.
        textGame = GameObject.Find("GameText").GetComponent<TextMeshProUGUI>();
    }

    // Task to do when button is clicked.
    void TaskOnClick()
    {
        if (TestPlayer.GetInstance().GetHealth() < TestPlayer.GetInstance().GetMaxHealth()) // [1]
        {
            Heal(20);
            textGame.SetText("I grow stronger! :D");
        }
    }

    void Heal(int health)
    {
        // Calculate new health value.
        newHealth = TestPlayer.GetInstance().GetHealth();
        TestPlayer.GetInstance().SetHealth(newHealth += health);
    }
}

// REFERENCES

/*[1] “Unity Tutorial: Preserving Data between Scene Loading/Switching...”. [Online].
Available: https://www.youtube.com/watch?v=WchH-JCwVI8.
[Accessed: 30-Jun.-2020].*/

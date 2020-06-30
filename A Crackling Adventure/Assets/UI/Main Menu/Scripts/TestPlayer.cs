using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Diagnostics;

public class TestPlayer : MonoBehaviour
{
    static TestPlayer instance; // [1]

    public static TestPlayer GetInstance()
    {
        return instance;
    }

    // This data can be saved to UserPref file later, if we implement it.
    protected int maxHealth = 100;
    protected int maxScore = 100;
    protected int currentHealth = 0;
    protected int currentScore = 0;

    // Start is called before the first frame update 
    void Start()
    {
        // Singleton pattern. He shall never die! https://wiki.unity3d.com/index.php/Singleton
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
        currentHealth = maxHealth;
        currentScore = maxScore;
    }
    
    // Setters and Getters (Java style still works, however C# has additional usage when using it's own format).
    public void SetScore(int score)
    {
        currentScore = score;
    }

    public int GetScore()
    {
        return currentScore;
    }

    public int GetMaxScore()
    {
        return maxScore;
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}

// REFERENCES

/*[1] “Unity Tutorial: Preserving Data between Scene Loading/Switching...”. [Online].
Available: https://www.youtube.com/watch?v=WchH-JCwVI8.
[Accessed: 30-Jun.-2020].*/

using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float gameOverDelay = 0.5f;

    public void GameOver() {
        if (gameHasEnded == false) {
            gameHasEnded = true;
            UnityEngine.Debug.Log("Game Over!");
            Invoke("Restart", gameOverDelay);
        }        
    }

    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Game Over");
    }
}

// REFERENCES

/*“GAME OVER - How to make a Video Game in Unity(E08) - YouTube”. [Online].
Available: https://www.youtube.com/watch?v=VbZ9_C4-Qbo.
[Accessed: 30-Jun.-2020].*/
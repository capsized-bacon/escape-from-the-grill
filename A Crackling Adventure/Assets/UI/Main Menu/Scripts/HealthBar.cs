    using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


// Placed on HealthBar Slider as script component
public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public HealthBar healthBar; // [1]

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    /*void Start() // This has been commented out because the maxHealth is set in Player.cs.
    {
        // healthBar.SetMaxHealth(Player.GetInstance().GetMaxHealth());
    }*/

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(Player.GetInstance().GetHealth());
    }
}

// REFERENCES

/*“[1] How to make a HEALTH BAR in Unity! - YouTube”. [Online].
Available: https://www.youtube.com/watch?v=BLfNP4Sc_iA.
[Accessed: 28-Jun.-2020].*/

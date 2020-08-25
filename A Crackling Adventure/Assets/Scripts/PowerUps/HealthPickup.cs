using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<Player>().ModifyHealth(50);
        FindObjectOfType<Player>().ModifyScore(50);
        FindObjectOfType<AudioManager>().Play("Ta");
        Destroy(gameObject);
    }

}
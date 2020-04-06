using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPotion : MonoBehaviour
{
    Health playerHealth;

    void Awake()
    {
        playerHealth = FindObjectOfType<Health>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerHealth.numOfHearts < 5)
        {
            Destroy(gameObject);
            playerHealth.numOfHearts++;
            playerHealth.health++;
        }
    }
}

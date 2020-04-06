using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    Health playerHealth;

    void Awake()
    {
        playerHealth = FindObjectOfType<Health>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerHealth.health < playerHealth.numOfHearts)
        {
            Destroy(gameObject);
            playerHealth.health++;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindyBullet : MonoBehaviour
{
    public GameObject player;
    public int damage;
    private void Start() {
        player = GameObject.FindWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name == "PlayerHolder") {
            player.GetComponent<Health>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}

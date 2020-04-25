using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    public GameObject item;
    public GameObject itemToEquip;
    //numbers of weapons 
    public int itemNumber;
    public float damage;
    private Transform player;
    public float thrust = 2;
    public float knockTime = 0.2f;


    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void SpawnDroppedItem() {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y + 1);
        Instantiate(item, playerPos, Quaternion.identity);

    }
}
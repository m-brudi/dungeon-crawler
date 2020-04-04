using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    public GameObject itemToEquip;

    //numbers of weapons 
    public int itemNumber;

    private Transform player;
    public float thrust = 2;
    public float knockTime = 0.2f;


    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void SpawnDroppedItem() {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y+1);
        Instantiate(item, playerPos, Quaternion.identity);

        //THROW WEAPON

        //Vector2 difference = player.position - Vector3.up;
        //difference = difference.normalized * thrust;
        //Rigidbody2D droppedItem = item.GetComponent<Rigidbody2D>();
        //droppedItem.isKinematic = false;
        //droppedItem.AddForce(difference, ForceMode2D.Impulse);
        //StartCoroutine(Throw(droppedItem));
        
    }

    private IEnumerator Throw(Rigidbody2D droppedItem) {
    yield return new WaitForSeconds(knockTime);
        droppedItem.velocity = Vector2.zero;
        droppedItem.isKinematic = true;
    }
}
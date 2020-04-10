using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public int openingDirection;
    //1 -> need down door
    //2 -> need up door
    //3 -> need left door
    //4 -> need right door
    public bool spawned = false;
    public float waitTime = 4f;

    private RoomTemplates templates;
    private int rand;
    private void Start() {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn() {
        
        if (spawned == false) {
            switch (openingDirection) {
                case 1:
                    // need down door
                    rand = Random.Range(0, templates.downRooms.Length);
                    Instantiate(templates.downRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 2:
                    // need up door
                    rand = Random.Range(0, templates.upRooms.Length);
                    Instantiate(templates.upRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 3:
                    //need left door
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 4:
                    //need right door
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
                    break;
                default:
                    // 
                    break;
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("SpawnPoint")) {
            try {
                if (!collision.GetComponent<RoomSpawner>().spawned && !spawned) {
                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            catch (System.Exception e) {
                Destroy(gameObject);
            }
            spawned = true;
            
        }    
    }
}

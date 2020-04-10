using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] downRooms;
    public GameObject[] upRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedEnd;
    public GameObject endObject;

    private void Update() {
        if(waitTime <=0 && !spawnedEnd) {
            Instantiate(endObject, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
        } else {
            waitTime -= Time.deltaTime;
        }
    }

}

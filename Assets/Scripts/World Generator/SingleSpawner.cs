using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] objects;
    public float probability;

    // Start is called before the first frame update
    void Start()
    {
        float randProb = Random.value;
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);
        int randObject = Random.Range(0, objects.Length);
        if(randProb <= probability) {
            Instantiate(objects[randObject], spawnPoints[randSpawnPoint].transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

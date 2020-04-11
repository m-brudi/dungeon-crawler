using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objects;
    public float probability = 0.7f;

    private void Start() {
        int rand = Random.Range(0, objects.Length);
        float randProbability = Random.value;
        if (Random.value < probability) {
            Instantiate(objects[rand], transform.position, Quaternion.identity);
        }
    }
}

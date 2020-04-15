using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : Enemy
{
    public float spawnRadius;
    public GameObject objectToSpawn;
    public GameObject spawnPoint;
    private GameObject player;
    public float spawnMin;
    public float spawnMax;
    private Vector2 position;

    public float spawnTimeMin;
    public float spawnTimeMax;

    private float diffX;
    private float diffY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        diffX = player.transform.position.x - transform.position.x;
        diffY = player.transform.position.y - transform.position.y;

        StartCoroutine(Spawn());

    }

    private void FixedUpdate() {
        diffX = player.transform.position.x - transform.position.x;
        diffY = player.transform.position.y - transform.position.y;

    }

    private void SpawnEnemies() {
        float enemiesNum = Random.Range(spawnMin, spawnMax);
        for (int i = 0; i < enemiesNum; i++) {
            position = new Vector2 (transform.position.x,transform.position.y);
            position.x += Random.Range(spawnRadius, -spawnRadius);
            position.y += Random.Range(spawnRadius, -spawnRadius);

            GameObject spawn = Instantiate(spawnPoint, position, Quaternion.identity);
            StartCoroutine(Wait(position, spawn));
            
        }
        if (Mathf.Abs(diffX) < 20 && Mathf.Abs(diffY) < 20) {
            StartCoroutine(Spawn());
        }
    }
    private IEnumerator Wait(Vector2 position, GameObject spawn) {
        yield return new WaitForSeconds(1);
        Instantiate(objectToSpawn, position, Quaternion.identity);
        Destroy(spawn);
    }
    private IEnumerator Spawn() {
        float waitTime = Random.Range(spawnTimeMin, spawnTimeMax);
        yield return new WaitForSeconds(waitTime);
        SpawnEnemies();
    }
}

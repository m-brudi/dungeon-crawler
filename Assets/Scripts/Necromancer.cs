using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    public float spawnRadius;
    public GameObject objectToSpawn;
    public float spawnMin;
    public float spawnMax;
    private Vector2 position;

    public float spawnTimeMin;
    public float spawnTimeMax;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemies() {
        float enemiesNum = Random.Range(spawnMin, spawnMax);
        for (int i = 0; i < enemiesNum; i++) {
            position = new Vector2 (transform.position.x,transform.position.y);
            position.x += Random.Range(spawnRadius, -spawnRadius);
            position.y += Random.Range(spawnRadius, -spawnRadius);
            //position = new Vector2(Random.Range(transform.position.x, spawnRadius), Random.Range(transform.position.y, spawnRadius));
            Instantiate(objectToSpawn, position, Quaternion.identity);
        }
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        float waitTime = Random.Range(spawnTimeMin, spawnTimeMax);
        yield return new WaitForSeconds(waitTime);
        SpawnEnemies();
    }
}

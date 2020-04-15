using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windy : Enemy
{
    public int numberOfBullets;
    public GameObject bullet;
    public GameObject player;
    public GameObject particles;
    public float moveSpeed;
    public float waitTime;
    public float range;
    public float speed;

    private float radius;
    Vector3 direction;
    private Vector3[] directions;
    void Start()
    {
        radius = 5f;
        moveSpeed = 5f;
        InvokeRepeating("SpawnProjectiles", 0.1f, waitTime);
        directions = new [] { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
        int rand = Random.Range(0, directions.Length);
        direction = directions[rand];
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate() {
        transform.Translate(direction * Time.deltaTime * speed);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(direction == Vector3.up) {
            direction = Vector3.down;
        } else if(direction == Vector3.down){
            direction = Vector3.up;
        } else if(direction == Vector3.left){
            direction = Vector3.right;
        } else {
            direction = Vector3.left;
        }
    }
    void SpawnProjectiles() {
        if (Vector2.Distance(player.transform.position, transform.position) <= range) {
            
            float angleStep = 360f / numberOfBullets;
            float angle = 0f;
            

            for (int i = 0; i <= numberOfBullets - 1; i++) {
                float projectileDirXposition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                float projectileDirYposition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

                Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
                Vector2 projectileMoveDirection = (projectileVector - new Vector2(transform.position.x,transform.position.y)).normalized * moveSpeed;

                var proj = Instantiate(bullet, transform.position, Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
                angle += angleStep;
            }
            Instantiate(particles, transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D enemyRB;
    private Transform target;
    private GameObject player;

    //for knockback
    public float thrust;
    public float knockTime;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "PlayerHolder") {

            //knockback
            enemyRB.isKinematic = false;
            Vector2 difference = transform.position - collision.transform.position;
            difference = difference.normalized * thrust;
            enemyRB.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockCo(enemyRB));
            player.GetComponent<Health>().TakeDamage(1);
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemyRB) {
        yield return new WaitForSeconds(knockTime);
        if (enemyRB != null) {
            enemyRB.velocity = Vector2.zero;
            enemyRB.isKinematic = true;
        }
    }
}
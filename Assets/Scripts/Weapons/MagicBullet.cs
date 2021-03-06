﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public GameObject greenhit;
    public GameObject player;
    public GameObject greenRange;
    public float attackRadius;
    public LayerMask whatIsEnemies;
    public int damage;
    public float knockTime;
    private GameObject range;

    private void Start() {
        player = GameObject.Find("PlayerHolder");
        damage = player.GetComponent<PlayerVariables>().greenMagicDamage;
    }
    void OnCollisionEnter2D(Collision2D collision) {
        range = Instantiate(greenRange, gameObject.transform.position, Quaternion.identity);
        Destroy(range, 0.5f);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRadius, whatIsEnemies);
        

        for (int i = 0; i < enemiesToDamage.Length; i++) {
            //knockback
            Rigidbody2D enemy = enemiesToDamage[i].GetComponent<Rigidbody2D>();
            enemiesToDamage[i].GetComponent<Enemy>().TakeDemage(damage);

            if (enemy != null) {
                Instantiate(greenhit, enemy.transform.position, Quaternion.identity);
                enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * 5;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemy));
            }
        }
        gameObject.GetComponent<Renderer>().enabled = false;
        Destroy(gameObject,1f);
    }
    private IEnumerator KnockCo(Rigidbody2D enemy) {
        yield return new WaitForSeconds(knockTime);
        if (enemy != null) {
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
        }
        
        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}

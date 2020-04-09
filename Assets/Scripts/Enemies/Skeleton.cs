using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton: Enemy
{
    public int damage;
    public Animator animator;
    public float attackDelay = 0.4f;
    public AudioClip swingSound;
    
    
    public GameObject player;
    public float chaseRadius;
    public Transform homePosition;
    public float moveSpeed;

    private Animator anim;
    private Rigidbody2D myRigidbody;
    private bool playerInRange = false;
    private bool walk = true;
    private bool seenPlayer = false;

    void Start() {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate() {
        float diff = player.transform.position.x - transform.position.x;
        if (diff > 0) {

            transform.localScale = new Vector3(1, 1, 1);
        }
        else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        CheckDistance();
    }

    void CheckDistance() {
        if(Vector3.Distance(player.transform.position, transform.position) <= 0.8f) {
            walk = false;
        }
        if (Vector3.Distance(player.transform.position, transform.position) <= chaseRadius && walk && !seenPlayer) {
            seenPlayer = true;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            anim.SetTrigger("Walking");
        }else if (seenPlayer && walk){
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            anim.SetTrigger("Walking");
        }
        else {
            anim.SetTrigger("notWalking");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerInRange = true;
            attack();

        }
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
           

        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerInRange = false;
            walk = true;
        }
    }

    public void attack() {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        AudioSource.PlayClipAtPoint(swingSound, transform.position);
        animator.SetTrigger("Attack");
        StartCoroutine(AttackDelay());

    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        CameraMove.Shake((transform.position - mousePos).normalized, 5f, 0.1f);
        Debug.Log("chuj");
        //damage the player
        if (playerInRange) {
            player.GetComponent<Health>().TakeDamage(damage);
            StartCoroutine(AttackAgain());
        }
    }
    private IEnumerator AttackAgain() {
        yield return new WaitForSeconds(0.9f);
        attack();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

}


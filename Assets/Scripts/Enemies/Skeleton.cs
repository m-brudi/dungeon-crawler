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
    public float seeRadius;
    public float ChaseRadius;
    public Vector2 homePosition;
    public float moveSpeed;

    private Animator anim;
    private Rigidbody2D myRigidbody;
    private bool playerInRange = false;
    private bool walk = true;
    private float diff;
    private bool doneWaiting = false;
    private float actionRadius;
    private float returnTime;
    void Start() {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        homePosition = new Vector2(transform.position.x, transform.position.y);
        actionRadius = seeRadius;
        
    }

    void FixedUpdate() {
        diff = player.transform.position.x - transform.position.x;
        CheckDistance();
    }

    void CheckDistance() {
        //if player in range for attack
        if (Vector3.Distance(player.transform.position, transform.position) <= 0.8f) {
            walk = false;
        }

        //if player in chase radius
        if (Vector3.Distance(player.transform.position, transform.position) <= actionRadius && walk) {

            actionRadius = ChaseRadius;

            if (diff > 0) {
                transform.localScale = new Vector3(1, 1, 1);
            } else {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            anim.SetTrigger("Walking");
            doneWaiting = false;
            StopCoroutine(WaitForReturn());

        //if player not in range and skeleton not close to home position
        } else if (Vector3.Distance(transform.position, homePosition) > 2) {

            StartCoroutine(WaitForReturn());

            if (doneWaiting) {
                if (homePosition.x - transform.position.x > 0) {
                    transform.localScale = new Vector3(1, 1, 1);
                } else {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                transform.position = Vector3.MoveTowards(transform.position, homePosition, moveSpeed * Time.deltaTime);
                walk = true;
                anim.SetTrigger("Walking");
            } else {
                anim.SetTrigger("notWalking");
            }
            //is in home position
        } else {
            doneWaiting = false;
            actionRadius = seeRadius;
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
        //damage the player
        if (playerInRange) {
            player.GetComponent<Health>().TakeDamage(damage);
            StartCoroutine(AttackAgain());
        }
    }

    //Time to wait for next attack
    private IEnumerator AttackAgain() {
        yield return new WaitForSeconds(0.9f);
        attack();
    }

    //Time to wait before goin back to homeposition
    private IEnumerator WaitForReturn() {
        returnTime = UnityEngine.Random.Range(3,5);
        yield return new WaitForSeconds(returnTime);
        doneWaiting = true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, actionRadius);
    }

}


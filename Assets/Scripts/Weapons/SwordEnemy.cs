using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : MonoBehaviour
{
    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask playerLayer;
    public int damage;
    public Animator animator;
    public float attackDelay = 0.1f;
    public AudioClip swingSound;

    public CameraMove CameraMove;
    public Camera cam;

    public float thrust;
    public float knockTime;


    public Vector3 mousePos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }
    }
    public void attack()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        AudioSource.PlayClipAtPoint(swingSound, transform.position);
        animator.SetTrigger("isAttacking");
        StartCoroutine(AttackDelay());

    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        Collider2D[] playerToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, playerLayer);

        CameraMove.Shake((transform.position - mousePos).normalized, 5f, 0.1f);

        for (int i = 0; i < playerToDamage.Length; i++)
        {
            //knockback
            Rigidbody2D player = playerToDamage[i].GetComponent<Rigidbody2D>();
            playerToDamage[i].GetComponent<Health>().TakeDamage(damage);

            if (player != null)
            {
                player.isKinematic = false;
                Vector2 difference = playerToDamage[i].transform.position - transform.position;
                difference = difference.normalized * thrust;
                player.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(player));
            }

        }
    }


    private IEnumerator KnockCo(Rigidbody2D player)
    {
        yield return new WaitForSeconds(knockTime);
        if (player != null)
        {
            player.velocity = Vector2.zero;
            player.isKinematic = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
    }
}

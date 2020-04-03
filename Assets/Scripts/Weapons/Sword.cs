using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask whatIsEnemies;
    public int damage;
    public Animator animator;
    public float attackDelay = 0.1f;

    public CameraMove CameraMove;
    public Camera cam;

    public float thrust;
    public float knockTime;


    public Vector3 mousePos;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            attack();
        }
    }
    public void attack() {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        animator.SetTrigger("Attack");
        StartCoroutine(AttackDelay());
        //Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);

        //CameraMove.Shake((transform.position - mousePos).normalized, 5f, 0.1f);

        //for (int i = 0; i < enemiesToDamage.Length; i++) {

        //    //knockback
        //    Rigidbody2D enemy = enemiesToDamage[i].GetComponent<Rigidbody2D>();
        //    enemiesToDamage[i].GetComponent<Enemy>().TakeDemage(damage);

        //    if (enemy != null) {
        //        enemy.isKinematic = false;
        //        Vector2 difference = enemy.transform.position - transform.position;
        //        difference = difference.normalized * thrust;
        //        enemy.AddForce(difference, ForceMode2D.Impulse);
        //        StartCoroutine(KnockCo(enemy));
        //    }
        //}
    }

    private IEnumerator AttackDelay() {
        yield return new WaitForSeconds(attackDelay);
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);

        CameraMove.Shake((transform.position - mousePos).normalized, 5f, 0.1f);

        for (int i = 0; i < enemiesToDamage.Length; i++) {
            //knockback
            Rigidbody2D enemy = enemiesToDamage[i].GetComponent<Rigidbody2D>();
            enemiesToDamage[i].GetComponent<Enemy>().TakeDemage(damage);

            if (enemy != null) {
                enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemy));
            }

        }
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
        Gizmos.DrawWireCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
    }
}

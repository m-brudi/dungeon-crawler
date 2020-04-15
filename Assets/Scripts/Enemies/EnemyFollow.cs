using System.Collections;
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
        float diffX = player.transform.position.x - transform.position.x;
        float diffY = player.transform.position.y - transform.position.y;
        if (Mathf.Abs(diffX) < 10 && Mathf.Abs(diffY) < 20) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
        if(diffX > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
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

        if(collision.gameObject.tag == "Enemy") {
            //knockback
            enemyRB.isKinematic = false;
            Vector2 difference = transform.position - collision.transform.position;
            difference = difference.normalized * 5;
            enemyRB.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockCo(enemyRB));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    Rigidbody2D myRb;
    public float thrust;

    private void Start() {
        myRb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "PlayerHolder") {
            //knockback
            myRb.isKinematic = false;
            Vector2 difference = transform.position - collision.transform.position;
            difference = difference.normalized * thrust;
            myRb.AddForce(difference, ForceMode2D.Impulse);
        }
    }

}

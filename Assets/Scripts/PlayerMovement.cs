using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 2f;
    public Rigidbody2D rb;
    public Camera cam;
    public Animator animator;
    public ParticleSystem dust;
    public GameObject dashSprite;

    public float dashSpeed;
    public float dashTime;
    //private Vector2 direction;

    public Vector3 movement;
    Vector3 mousePos;


    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //rotate player to mouse position
        if (mousePos.x < rb.position.x) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (mousePos.x > rb.position.x) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //walking animation
        if (movement != Vector3.zero) {
            animator.SetBool("isWalking", true);
        }
        else {
            animator.SetBool("isWalking", false);
            CreateDust();
        }
        
        //DASH
        if(movement!=Vector3.zero && Input.GetKeyDown("space")){
            StartCoroutine("DashMove");

        }
    }

    void FixedUpdate() {
        movement.Normalize();
        rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void CreateDust() {
        dust.Play();
    }


    IEnumerator DashMove() {
        dashSprite.GetComponent<Renderer>().enabled = true;
        moveSpeed += dashSpeed;
        yield return new WaitForSeconds(dashTime);
        dashSprite.GetComponent<Renderer>().enabled = false;
        moveSpeed -= dashSpeed;
    }
}

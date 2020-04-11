using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    Animator anim;
    public GameObject flameLight;
    private void Start() {
        anim = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            anim.SetTrigger("Fire");
        }
        flameLight.SetActive(true);
    }
}

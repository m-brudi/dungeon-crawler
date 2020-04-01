using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public string enemyName;
    public int baseAttack;

    public GameObject effect;
    public GameObject blood;
    public SpriteRenderer body;
    public Color hurtColor;
    public CameraMove CameraMove;
    public Camera cam;
    public Vector3 mousePos;


    // Update is called once per frame
    public void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (health <= 0)
        {
            //particles
            Instantiate(effect, transform.position, Quaternion.identity);
            //bloodstain
            Instantiate(blood, transform.position, Quaternion.identity);
            //shake
            CameraMove.Shake((transform.position - mousePos).normalized, 20f, 0.1f);

            Destroy(gameObject);
        }
    }
    public IEnumerator Flash()
    {
        body.color = hurtColor;
        yield return new WaitForSeconds(0.1f);
        body.color = Color.white;
    }

    public void TakeDemage(int damage)
    {
        StartCoroutine(Flash());
        health -= damage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public GameObject greenhit;
    void OnCollisionEnter2D(Collision2D collision) {
        Instantiate(greenhit, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}

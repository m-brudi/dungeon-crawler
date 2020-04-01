using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpellGun : MonoBehaviour
{
    public float offset = 270;
    public GameObject projectile;
    public Transform shotPoint;
    public float bulletSpeed;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private float timeBtwShots;
    public float startTimeBtwShots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotation of imaginary weapon (cause nothing is rotating)
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPoint.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        shotPoint.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0) {
            if (Input.GetMouseButtonDown(0)) {
                GameObject bullet = Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ + 180));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                StartCoroutine("HideMagicGun");

                rb.AddForce(shotPoint.up * bulletSpeed, ForceMode2D.Impulse);
                Destroy(bullet, 2f);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }
    }
    IEnumerator HideMagicGun() {
        //GetComponent<Renderer>().enabled = false;
        animator.SetTrigger("appear");
        //spriteRenderer.color = new Color(1f, 1f, 1f, .2f);
        yield return new WaitForSeconds(1);
        //spriteRenderer.color = Color.white;
        animator.Play("Idle");
    }
}

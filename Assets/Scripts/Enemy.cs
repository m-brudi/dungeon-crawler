using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public int health;
    public string enemyName;
    public int baseAttack;
    public AudioClip hitSound;
    public AudioClip deathSound;

    public GameObject effect;
    public GameObject blood;
    public SpriteRenderer body;
    public Color hurtColor;
    public CameraMove CameraMove;
    public Camera cam;
    public Vector3 mousePos;

    private void Start() {
        
    }
    // Update is called once per frame
    public void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (health <= 0)
        {
            //sound
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            
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
        PlayClipAt(hitSound, transform.position);
        StartCoroutine(Flash());
        health -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name == "GreenBullet(Clone)") {
            //TakeDemage(collision.gameObject.GetComponent<MagicBullet>().damage);
        }
    }

    //experimenting with audio effects
    //basically custom PlayClipAtPoint method;
    AudioSource PlayClipAt(AudioClip clip, Vector3 pos) {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
                             // set other aSource properties here, if desired
        aSource.pitch = Random.Range(1, 1.5f);
        aSource.volume = 0.1f;
        aSource.Play(); // start the sound
        Destroy(tempGO, clip.length); // destroy object after clip duration
        return aSource; // return the AudioSource reference
    }
}

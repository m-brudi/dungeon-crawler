using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health = 3;
    public int numOfHearts = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite HalfHeart;
    public Sprite EmptyHeart;
    public SpriteRenderer body;

    private void Start() {
        
    }
    private void Update() {

        if (health > numOfHearts) {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++) {
            if (i < health) {
                hearts[i].sprite = fullHeart;
            }
            else {
                hearts[i].sprite = EmptyHeart;
            }

            if(i < numOfHearts) {
                hearts[i].enabled = true;
            }
            else {
                hearts[i].enabled = false;
            }
        }

        if(health < 1) {
            body.enabled = false;
            StartCoroutine(Death());
           
        }

    }

    public IEnumerator Death() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }

    public IEnumerator Flash() {
        body.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        body.color = Color.white;
    }
    public void TakeDamage(int damage) {
        health -= damage;
        StartCoroutine(Flash());

    }

}

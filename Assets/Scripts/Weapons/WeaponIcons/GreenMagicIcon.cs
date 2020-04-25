using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GreenMagicIcon : MonoBehaviour
{
    public int damage;
    private GameObject player;
    public Text text;

    void Start() {
        player = GameObject.Find("PlayerHolder");
        damage = player.GetComponent<PlayerVariables>().greenMagicDamage;
        text.text = "DMG \n" + damage;
    }

    public void updateDamage() {
        damage = player.GetComponent<PlayerVariables>().greenMagicDamage;
        text.text = "DMG \n" + damage;
    }
}

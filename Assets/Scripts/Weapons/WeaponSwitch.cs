using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] slots;
    private int activeSlot;
    private Inventory inventory;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update() {
        activeSlot = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().activeSlot;

        if (slots[activeSlot].transform.childCount > 0) {
            ShowWeapon(activeSlot);
        }

        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].transform.childCount > 0) {
                if (i != activeSlot) {
                    HideWeapon(i);
                }
            }
        }
    }

    public void HideWeapon(int i) {
        gameObject.transform.GetChild(slots[i].transform.GetChild(0).gameObject.GetComponent<Spawn>().itemNumber).gameObject.SetActive(false);
    }
    public void ShowWeapon(int i) {
        gameObject.transform.GetChild(slots[i].transform.GetChild(0).gameObject.GetComponent<Spawn>().itemNumber).gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public bool[] isFull;
    public GameObject[] slots;
    public int activeSlot = 0;
    public string weaponToEquip;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            activeSlot = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            activeSlot = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            activeSlot = 2;
        }

        for (int i = 0; i < slots.Length; i++) {
            if (i == activeSlot) {
                slots[activeSlot].GetComponent<Image>().color = Color.green;
            }
            else {
                slots[i].GetComponent<Image>().color = Color.white;
            }
        }
        //weaponToEquip = slots[activeSlot].transform.GetChild(0).gameObject.GetComponent<Spawn>().itemToEquip;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public float speed = 50f;
    private Inventory inventory;
    public GameObject itemButton;
    private int activeSlot;
    private bool allTaken = false;
    private bool weaponTaken = false;


    // Start is called before the first frame update
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

    }

    // Update is called once per frame
    void Update()
    {
        activeSlot = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().activeSlot;
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);

        //check if all slots are taken  
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i])
            {
                allTaken = true;
            }
            else
            {
                allTaken = false;
                break;
            }
        }

    }


    //put items in empty slots
    //if all slots are taken put item in selected slot
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++) {
                if (inventory.slots[i].transform.childCount > 0) {
                    if (inventory.slots[i].transform.GetChild(0).gameObject.name == (itemButton.gameObject.name + "(Clone)")) {
                        weaponTaken = true;
                    //} else {
                    //    weaponTaken = true;
                    }
                }
            }
            for (int i = 0; i < inventory.slots.Length; i++) {

                if (inventory.isFull[i] == false && !weaponTaken) {
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                } else if (allTaken && Input.GetKey(KeyCode.LeftShift)&& !weaponTaken) {
                    //if all slots taken and lshift clicked switch items    
                    //spawn the dropped object back to the scene
                    GameObject.Find("Weapons").GetComponent<WeaponSwitch>().HideWeapon(activeSlot);
                    inventory.slots[activeSlot].transform.GetChild(0).gameObject.GetComponent<Spawn>().SpawnDroppedItem();

                    Destroy(inventory.slots[activeSlot].transform.GetChild(0).gameObject);
                    Instantiate(itemButton, inventory.slots[activeSlot].transform, false);
                    Destroy(gameObject);

                    //prevent from switching back and forth
                    StartCoroutine(Wait());
                    weaponTaken = false;
                    break;
                }
            }
        }

    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(.3f);

    }
}
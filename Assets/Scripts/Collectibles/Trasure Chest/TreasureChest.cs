﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreasureChest : MonoBehaviour
{
    public GenericLootDropTableGameObject lootDropTable;
    public int numItemsToDrop;
    private Animator anim;
    private int rndPosX;
    private bool used = false;

    System.Random rnd = new System.Random();

    

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnValidate()
    {
        lootDropTable.ValidateTable();
    }

    // Update is called once per frame

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            anim.SetBool("isClosed", true);
            anim.SetBool("isOpen", false);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            anim.SetBool("isClosed", false);
            anim.SetBool("isOpen", true);
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        rndPosX = rnd.Next(1, 3);
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.LeftShift) && !used) {
                used = true;
                anim.SetBool("isEmpty", true);
                DropLootNearChest(numItemsToDrop);
        }

    }

    private void DropLootNearChest(int numItemsToDrop)
    {
        for(int i = 0; i < numItemsToDrop; i++)
        {
            GenericLootDropItemGameObject selectedItem = lootDropTable.PickLootDropItem();
            GameObject selectedItemGameObject = Instantiate(selectedItem.item);
            selectedItemGameObject.transform.position = new Vector3(transform.position.x + 2, transform.position.y);
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 movement;
    public Animator animator;
    PlayerMovement Script;
    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerHolder = GameObject.Find("PlayerHolder");
        Script = PlayerHolder.GetComponent<PlayerMovement>();

        movement = Script.movement;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Script.movement);
        if (Script.movement != Vector3.zero) {
            animator.SetBool("isWalking", true);
        }
        else {
            animator.SetBool("isWalking", false);
        }
    }
}

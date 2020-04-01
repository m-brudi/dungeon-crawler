using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSprite : MonoBehaviour
{
    PlayerMovement Script;
    Vector3 movement;
    public GameObject player;
    // Start is called before the first frame update
    void Start() {
        GameObject PlayerHolder = GameObject.Find("PlayerHolder");
        Script = PlayerHolder.GetComponent<PlayerMovement>();
        transform.position = player.transform.position;
        //movement = Script.movement;
        
    }
    void Update() {
        movement = Script.movement;
        transform.position = player.transform.position;

        if (movement.x > 0 && movement.y > 0) {
            RotateSprite(225);
        }else if(movement.x > 0 && movement.y == 0) {
            RotateSprite(180);
        }else if(movement.x > 0 && movement.y < 0){
            RotateSprite(135);
        }else if(movement.x == 0 && movement.y > 0){
            RotateSprite(270);
        }else if(movement.x == 0 && movement.y < 0){
            RotateSprite(90);
        }else if(movement.x < 0 && movement.y > 0) {
            RotateSprite(315);
        }else if(movement.x<0 && movement.y == 0) {
            RotateSprite(0);
        }else if(movement.x < 0 && movement.y < 0) {
            RotateSprite(45);
        }
    }

    private void RotateSprite(int degree) {
        transform.eulerAngles = new Vector3(0, 0, degree);
    }
}

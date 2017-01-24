using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;             //Floating point variable to store the player's movement speed.
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    private int count; //먹은 힌트 수
    public Text countText;
    public Text winText;
    private Animator[] anim;

    // Use this for initialization
    void Start() {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponentsInChildren<Animator>();
        count = 0;
        winText.text = "";
        setCountText();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate() {

        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);

        if (moveHorizontal > 0) {
            anim[2].SetFloat("Pressed", moveHorizontal);
        } else if(moveHorizontal < 0) {
            anim[1].SetFloat("Pressed", moveHorizontal);            
        } else {
            anim[2].SetFloat("Pressed", moveHorizontal);
            anim[1].SetFloat("Pressed", moveHorizontal);  
        }

        Debug.Log(moveHorizontal);

        anim[0].SetFloat("Speed", rb2d.velocity.SqrMagnitude());
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.CompareTag ("Hint")) {
            other.gameObject.SetActive (false);
            count++;
            setCountText();
        }
    }

    //update the current count string
    void setCountText() {
        countText.text = "Hint: " + count.ToString();
        if (count >= 4) {
        	winText.text = "Completed";
        }
    }

}
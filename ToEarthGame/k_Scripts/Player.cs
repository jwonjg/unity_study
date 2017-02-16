using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;             //Floating point variable to store the player's movement speed.
    // private Animator[] anims;
    private Animator leftFireAnim;
    private Animator rightFireAnim;
    private Animator playerAnim;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public Text cntText;
    public Text winText;
    private int countHint;
    private int totalHint;

    private bool leftPressed;
    private bool rightPressed;
    private PressCheck left;
    private PressCheck right;

    // Use this for initialization
    void Start() {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        leftFireAnim = transform.Find("leftFire").GetComponent<Animator>();
        rightFireAnim = transform.Find("rightFire").GetComponent<Animator>();
        playerAnim = GetComponent<Animator>();

        totalHint = GameObject.FindGameObjectsWithTag("Hint").Length;
        winText.text = "";
        countHint = 0;
        setCountText();

        GameObject leftCheck = GameObject.Find("leftHand");
        GameObject rightCheck = GameObject.Find("rightHand");
        left = leftCheck.GetComponent <PressCheck> ();
        right = rightCheck.GetComponent <PressCheck> ();
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
//        Debug.Log(moveHorizontal);
        leftPressed = left.pressed;
        rightPressed = right.pressed;
        Debug.Log("left:" + leftPressed);
        Debug.Log("right:" + rightPressed);
        if(rightPressed || leftPressed) {
        	rb2d.AddForce(movement * speed);
        }

        leftFireAnim.SetFloat("Speed", rb2d.velocity.SqrMagnitude());
        rightFireAnim.SetFloat("Speed", rb2d.velocity.SqrMagnitude());

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightFireAnim.SetBool("Right", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightFireAnim.SetBool("Right", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftFireAnim.SetBool("Left", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftFireAnim.SetBool("Left", false);
        }
/*
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
*/
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.CompareTag ("Hint")) {
            //other.gameObject.SetActive (false);
            countHint++;
            setCountText();
        } else if (other.gameObject.CompareTag ("Obstacle")) {
            playerAnim.SetTrigger("DieTrigger");
        }

    }

    //update the current count string
    void setCountText() {
        cntText.text = "Hint: " + countHint.ToString();
        if (countHint == totalHint) {
        	winText.text = "Completed";
        }
    }

    void onDieAnimationEnd() {
        gameObject.SetActive(false);
        winText.text = "Game Over";
    }

}
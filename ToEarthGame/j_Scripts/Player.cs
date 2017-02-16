using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float speed;             //Floating point variable to store the player's movement speed.
    private Animator leftFireAnim;
    private Animator rightFireAnim;
    private Animator playerAnim;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public Text cntText;
    public Text winText;
    private int countHint;
    private int totalHint;
    
    private ControllerLeft controllerLeft;
    private ControllerLeft controllerRight;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        // 자식 GameObject를 찾아 각각에 설정된 Animator 컴포넌트를 얻음
        // TODO: 각 GameObject 하위 스크립트로 분리하는 방향도 검토할 것
        leftFireAnim = transform.Find("LeftFire").GetComponent<Animator>();
        rightFireAnim = transform.Find("RightFire").GetComponent<Animator>();
        playerAnim = GetComponent<Animator>();

        totalHint = GameObject.FindGameObjectsWithTag("HintTag").Length;
        Debug.Log(totalHint);
        winText.text = "";
        countHint = 0;
        setHintText();

        controllerLeft = GameObject.Find("ControllerLeft").GetComponent<ControllerLeft>();
        controllerRight = GameObject.Find("ControllerRight").GetComponent<ControllerLeft>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        //rb2d.AddForce(movement * speed);

        if (controllerLeft.IsPressed())
        {
            rb2d.AddForce(new Vector2(-speed, speed));
        }

        if (controllerRight.IsPressed())
        {
            rb2d.AddForce(new Vector2(speed, speed));
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
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("HintTag")) {
            countHint++;
            setHintText();
        } else if (collision.gameObject.CompareTag("ObstacleTag"))
        {
            playerAnim.SetTrigger("DieTrigger");
        }
    }

    void onDieAnimationEnd()
    {
        // TODO: 게임 종료 시 
        gameObject.SetActive(false);
        setGameOverText();
    }

    void setHintText()
    {
        cntText.text = "hint : " + countHint.ToString();
        if (countHint == totalHint)
        {
            winText.text = "complete!";
        }
    }

    void setGameOverText()
    {
        winText.text = "game over!";
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Animator leftHandleAnim;
    private Animator rightHandleAnim;
    private Vector3 offset;
    
	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
        leftHandleAnim = transform.Find("leftHand").GetComponent<Animator>();
        rightHandleAnim = transform.Find("rightHand").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
//        Debug.Log(moveHorizontal);
        if (moveHorizontal > 0) {
            rightHandleAnim.SetFloat("Pressed", moveHorizontal);
        } else if(moveHorizontal < 0) {
            leftHandleAnim.SetFloat("Pressed", moveHorizontal);            
        } else {
            rightHandleAnim.SetFloat("Pressed", moveHorizontal);
            leftHandleAnim.SetFloat("Pressed", moveHorizontal);  
        } 
    }

    // runs after procedural animation and gathering last known states
    // guaranteed to run after all items have been processed in Update
    void LateUpdate() {
        transform.position = player.transform.position + offset;
    }
}

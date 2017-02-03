using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public GameObject obstacleManager;

    private Vector3 offset;
    
	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
    }

    // runs after procedural animation and gathering last known states
    // guaranteed to run after all items have been processed in Update
    void LateUpdate() {
        transform.position = player.transform.position + offset;
        obstacleManager.transform.Translate(player.transform.position - obstacleManager.transform.position);
    }
}

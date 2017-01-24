using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

    public GameObject[] obstacles;
    public int obstacleCount;

    private float cameraWidth;
    private float cameraHeight;

    // Use this for initialization
    void Start () {

        // Debug.Log(Camera.main.orthographicSize);
        // Debug.Log("transform.localPosition.y:" + transform.localPosition.y);

        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;
        cameraHeight = Camera.main.orthographicSize * 2f;

        for(int i = 0; i < obstacleCount; i++)
        {
            float x = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
            float y = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
            Instantiate(obstacles[0], new Vector3(x, y), transform.rotation);
        }

        // Instantiate(obstacles[0], new Vector3(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize), transform.rotation);
        // Instantiate(obstacles[1], transform.localPosition, transform.rotation);
        // Debug.Log("transform.rotation:" + transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}

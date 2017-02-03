using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

    public GameObject[] obstacles;
    public int obstacleCount;
    
    private float cameraWidth;
    private float cameraHeight;
    private Bounds colliderBounds;

    private List<GameObject> obstacleInstances;

    // Use this for initialization
    void Start () {

        // Debug.Log(Camera.main.orthographicSize);
        // Debug.Log("transform.localPosition.y:" + transform.localPosition.y);
        
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        cameraHeight = Camera.main.orthographicSize;
        colliderBounds = GetComponent<BoxCollider2D>().bounds;

        obstacleInstances = new List<GameObject>();

        initObstacleInstances();

        // Instantiate(obstacles[0], new Vector3(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize), transform.rotation);
        // Instantiate(obstacles[1], transform.localPosition, transform.rotation);
        // Debug.Log("transform.rotation:" + transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("ObstacleTag"))
        {
            Vector3 position = collision.gameObject.transform.position - Camera.main.transform.position;
            
            if (position.y < colliderBounds.min.y)
            {
                position.x = Random.Range(-cameraWidth, cameraWidth);
                position.y = Random.Range(cameraHeight, colliderBounds.max.y);
            }
            else if (position.y > colliderBounds.max.y)
            {
                position.x = Random.Range(-cameraWidth, cameraWidth);
                position.y = Random.Range(colliderBounds.min.y, -cameraHeight);
            }
            else if (position.x < colliderBounds.min.x)
            {
                position.x = Random.Range(cameraWidth, colliderBounds.max.x);
                position.y = Random.Range(-cameraHeight, cameraHeight);
            }
            else if (position.x > colliderBounds.max.x)
            {
                position.x = Random.Range(colliderBounds.min.x, -cameraWidth);
                position.y = Random.Range(-cameraHeight, cameraHeight);
            }
            collision.gameObject.transform.position = position + Camera.main.transform.position;
        }
        
    }

    private void initObstacleInstances()
    {
        GameObject clone;
        for (int i = 0; i < obstacleCount; i++)
        {
            float x = Random.Range(0, cameraWidth * 2f / 3f);
            float y = Random.Range(0, cameraHeight * 2f / 3f);
            if(Random.Range(0, 2) < 1)
            {
                x = cameraWidth - x;
            } else
            {
                x -= cameraWidth;
            }
            if (Random.Range(0, 2) < 1)
            {
                y = cameraHeight - y;
            } else
            {
                y -= cameraHeight;
            }
            clone = Instantiate(obstacles[0], new Vector3(x, y), transform.rotation);
            obstacleInstances.Add(clone);
        }

    }
    
}

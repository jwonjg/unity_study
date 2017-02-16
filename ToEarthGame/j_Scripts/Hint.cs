using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour {

    public GameObject explosion;

    private ParticleSystem hintParticle;

	// Use this for initialization
	void Start () {
        hintParticle = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, 40)*Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTag"))
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GameObject explosionObj = Instantiate(explosion, transform.position, Quaternion.identity);
            StartCoroutine(MoveParticles(explosionObj));
        }
    }

    private IEnumerator MoveParticles(GameObject explosionObj)
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log(explosionObj);

        //explosionObj.GetComponent<ParticleSystem>().Pause();
        explosionObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(50.0f, 50.0f) * 10f);
        
        gameObject.SetActive(false);
    }
}

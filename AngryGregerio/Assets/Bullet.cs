using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 30f;
    public float power = 12f;
    public float life = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;
        if(life <= 0f)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}

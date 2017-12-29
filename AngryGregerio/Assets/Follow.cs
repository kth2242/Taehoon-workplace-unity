using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public GameObject target;
    public float distance = 5f;
    public float height = 8f;
    public float speed = 2f;

    Vector3 pos;

	// Update is called once per frame
	void Update () {
        pos = new Vector3(target.transform.position.x, height, target.transform.position.z - distance);
        this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, pos, speed * Time.deltaTime);
	}
}

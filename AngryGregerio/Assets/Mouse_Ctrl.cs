using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Ctrl : MonoBehaviour {

    public Transform target;
    public GameObject curser;
    public Player_Ctrl PC;
    	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            curser.transform.position = new Vector3(hit.point.x, 1f, hit.point.z);
            target.position = new Vector3(hit.point.x, PC.gameObject.transform.position.y, hit.point.z);
            PC.lookDirection = target.position - PC.gameObject.transform.position;

            if (Input.GetMouseButtonDown(0) && PC.PS != PLAYER_STATE.DEAD)
            {                
                PC.StartCoroutine("Shot");
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRange : TamskManager {


	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnDestroy()
    {
        GameObject[] tempobj = GameObject.FindGameObjectsWithTag("Board");
        foreach (GameObject ob in tempobj)
        {
            ob.gameObject.GetComponent<BoardManager>().moveAble = false;
        }

    }
}

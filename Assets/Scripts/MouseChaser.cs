using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChaser : TamskManager {


    private Vector3 mousePos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,-Camera.main.transform.position.z-6));
        transform.position = mousePos;
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
           
        }
        else { isClicked = false; }
        //Debug.Log(mousePos);
    }
   public void onClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(mousePos);
        }
       
    }
}

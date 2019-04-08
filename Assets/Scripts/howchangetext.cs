using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class howchangetext : changeclass {

	public GameObject[] gameobject;
	// Use this for initialization
	void Start () {
		for (int ii = 0; ii < 5; ii++) {
			gameobject [ii].SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int ii = 0; ii < 5; ii++) {
			if (ii != i)
				gameobject [ii].SetActive (false);
			else {
				if(gameobject[i].activeSelf==false) gameobject [i].SetActive (true);
			}
		}
	}
	public void clicknext()
	{
		if(i<4) i++;
	}
	public void clickpre()
	{
		if (i > 0) i--;
	}
}

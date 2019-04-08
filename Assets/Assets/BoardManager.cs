using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : TamskManager {
    public int ringCount=0;
    public int maxRingCount;
    public int gridId=0;
    public bool cursorExist = false;
    public bool moveAble = false;
    public GameObject obj;
    public bool start = true;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ringCnt = GameObject.FindGameObjectsWithTag("MoveAble");
        if (ringCnt.Length == 0) {
            moveAble = false;
        }
        if (isClicked == true && cursorExist==true)
        {
            
            //Debug.Log(moveAble + "//" + ringCnt.Length);
            if (moveAble == true&&ringCnt.Length==1)
             {
                //float x=GetComponent<Transform>().position.x;
                //Debug.Log(obj.name);
                GameObject parent;
                parent = obj.transform.parent.gameObject;
                if (baton == 2) {
                    if (parent.tag == "Player2") {
                        if(ccnt==0&&acnt==0)
                        {
                            Debug.Log("2패배임");
                        }
                        //timerExist[gridId] = false;
                        parent.transform.position = transform.position;
                        batonchange ();
                        if (parent.gameObject.GetComponent<GameTimer>().timerStarted == false)
                        {
                            parent.gameObject.GetComponent<GameTimer>().startTimer(parent.gameObject.GetComponent<GameTimer>().secondsRemaining);
                            parent.gameObject.GetComponent<GameTimer>().timerStarted = true;
                        }
                        else
                        {
                            parent.GetComponent<GameTimer>().timeInvert();
                        }
                        
                        Destroy (obj);
                        Brings--;
                        acnt = 0;
                        ccnt = -1;
                        
                    } else {
                        parent = obj.transform.parent.gameObject;
                    }
                } else {
                    if (parent.tag == "Player1") {
                        //timerExist[gridId] = false;
                        if (ccnt == 0 && acnt == 0)
                        {
                            Debug.Log("1패배임");
                        }
                        parent.transform.position = transform.position;
                        batonchange ();
                        if (parent.gameObject.GetComponent<GameTimer>().timerStarted == false)
                        {
                            parent.gameObject.GetComponent<GameTimer>().startTimer(parent.gameObject.GetComponent<GameTimer>().secondsRemaining);
                            parent.gameObject.GetComponent<GameTimer>().timerStarted = true;
                        }
                        else
                        {
                            parent.GetComponent<GameTimer>().timeInvert();
                        }
                        Destroy (obj);
                        Rrings--;
                        acnt = 0;
                        
                    } else {
                        parent = obj.transform.parent.gameObject;
                    }
                }
            }
            //Debug.Log(ringCnt.Length);

        }/*
        if (ringCount >= maxRingCount && timerExist[gridId] == false)
        {
            this.gameObject.SetActive(false);
        }*/ 
        if (moveAble == true)
        {
            if (ringCount >= maxRingCount)
            {
                moveAble = false;
                this.gameObject.SetActive(false);
            }
            else {this.GetComponent<MeshRenderer>().material.color = new Color(1,0,0.7f,1);     
            }
            

        }
        else
        {
            switch (ringCount)
            {
                case 0: this.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1); break;
                case 1: this.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 0.5f, 1); break;
                case 2: this.GetComponent<MeshRenderer>().material.color = new Color(0.8f, 0.8f, 0.3f, 1); break;
                case 3: this.GetComponent<MeshRenderer>().material.color = new Color(0.6f, 0.6f, 0.2f, 1); break;
                case 4: this.GetComponent<MeshRenderer>().material.color = new Color(0.4f, 0.4f, 0, 1); break;
            }
        }
	}
    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("??" + col.gameObject.name);
        if (col.tag == "Player1" || col.tag == "Player2")
        {
            col.gameObject.GetComponent<GameTimer>().currentBoard = this;
            timerExist[gridId]= true;
            moveAble = false;
            if (start!=false)
            {
                ringCount++;
            }
        }

        if (col.tag == "Chaser")
        {
            /*if (timerExist[gridId] == true)
            {*/
                cursorExist = true;
            //}
        }
        if (col.tag == "MoveAble")
        {
            obj = col.gameObject;
            if (timerExist[gridId] == false)
            {
                moveAble = true;
            }
            else { moveAble = false; }
        }
    }
    
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player1" || col.tag == "Player2")
        {
            timerExist[gridId] = true;
            moveAble = false;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player1" || col.tag == "Player2")
        {
            start = true;
            timerExist[gridId] = false;
        }
        if (col.tag == "Chaser")
        {
            cursorExist = false;
        }
        /*if (col.tag == "MoveAble")
        {
            Debug.Log("false");
            moveAble = false;
        }*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TamskManager : MonoBehaviour {
    public static int ccnt = 0; //갯수 확인
    public static int acnt = 0; //갯수 확인
    public static int bcnt = 0;//  오브젝트 확인
    public static int Rrings = 32;
    public static int Brings = 32;
    public static bool isClicked = false;
    public static bool[] timerExist = new bool[37];
    public static GameObject[] ringCnt;
    public static int baton=1;
    public static int batonBan = 0;
    public Text player1_ring;
    public Text player2_ring;
    public void batonchange()
    {
        if (baton == 1)
        {
            
            baton = 2;
            
        }
        else if(baton==2){
            baton = 1;
        }
        if (batonBan == baton)
        {
            batonBan = 0;
        }
    }
    private void Awake()
    {
        ringCnt = GameObject.FindGameObjectsWithTag("MoveAble");
        for (int i = 0; i < 36; i++)
        {
            timerExist[i] = false;
        }
        
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		player1_ring.GetComponent<Text>().text = " : " + Rrings;
		player2_ring.GetComponent<Text>().text = " : " + Brings;
	}
}

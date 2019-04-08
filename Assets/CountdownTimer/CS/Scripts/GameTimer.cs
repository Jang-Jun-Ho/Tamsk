using UnityEngine;
using System.Collections;

public class GameTimer : TamskManager {

	//this is how close to being done before it does the bounce animation (.8 means 80% done)
	public float bouncePerc = 0.8f;
	//what would you like to do when the time runs out
	public GameObject sendMessageObject;
	public string sendMessage;
	
	//start immediately when the scene runs
	public bool startOnPlay;
	public float secondsRemaining = 16;
	
	//define elements of the clocks\
	public GameObject hand;
	public GameObject activeR;
	public GameObject inacHalf;
	public Renderer activeRightRenderer;
	public Renderer inactiveHalfRenderer;
	
	//private vars
	private float maxTime;
	private float timeLeft;
	private GameObject thisGO;
	private Transform handT;
	private bool timerIsRunning;
    public bool timerStarted;
	
	//event listeners
	public delegate void TimeIsUpAction();
	public static event TimeIsUpAction TimeIsUp;


    public BoardManager currentBoard;
    public GameObject range;
    public bool isEnabled = false;
    public bool timer15Started = false;

	void Start () {
	
		thisGO = gameObject;
		handT = hand.transform;
		
		if(startOnPlay == true){
			startTimer(secondsRemaining);
		}
	
	}
    public void timer15()
    {
        if (timer15Started == false)
        {
            timer15Started = true;
        }
        Debug.Log(batonBan);
    }
    void TimeCheck()
    {
        if (isEnabled ==true)
        {
            startTimer(secondsRemaining);
        }
    }
	
	public void startTimer(float timerSeconds)
	{
		
		
		
		maxTime = timerSeconds;
		timeLeft = timerSeconds;
		resumeTimer();
		stopTimer();//make sure it's not already playing
		timerIsRunning = true;
		InvokeRepeating("moveTime", 1f, 1f);//start timer
		
	}
	
	//this function runs every second
	private void moveTime()
	{
		
		timeLeft = timeLeft - 1;
		
		determineAngle();
		
	}
	
	private void determineDone()
	{
		
		//check if 0 seconds left
		if(timeLeft == 0){
			
			stopTimer(true);
			
			timerIsRunning = false;
			
			if(sendMessageObject){
				
				sendMessageObject.SendMessage(sendMessage);
				
			}
			
			if(TimeIsUp != null)
				TimeIsUp();
			
		}
		
	}
	
	//add seconds
	public void addSeconds(int timeToAdd)
	{
		
		if(timerIsRunning){
			timeLeft = timeLeft + timeToAdd;
			if(timeLeft > maxTime){
				timeLeft = maxTime;
			}
			determineAngle();
		}
		
	}

    public void timeInvert()
    {
        timeLeft = maxTime - timeLeft;
        determineAngle();
    }
    

    //determine the angle of the hand
    private void determineAngle()
	{
		
		float percDone = 1 - timeLeft/maxTime;
		float angle = -percDone * 360;
		if(percDone <= 0)angle = 359;
		
		moveHands(percDone, angle);
		
	}
	
	//move the hands
	private void moveHands(float percDone, float angle)
	{
		
		//iTween.RotateTo(hand,{"rotation":Vector3(0,0,angle),"time":.5,"delay":0, "onComplete":"determineDone", "onCompleteTarget":thisGO, "onUpdate":"moveBgs", "onUpdateTarget":thisGO});
		//iTween.MoveTo(gameObject,iTween.Hash("x",3,"time",4,"delay",1,"onupdate","myUpdateFunction","looptype",iTween.LoopType.pingPong));				
		iTween.RotateTo(hand, iTween.Hash("rotation",new Vector3(0,0,angle),"time",.5f,"delay",0, "onComplete","determineDone", "onCompleteTarget",thisGO, "onUpdate","moveBgs", "onUpdateTarget",thisGO));
		
		//iTween.RotateTo(hand, iTween.Hash("rotation",new Vector3(0,0,angle),"time",.5f,"delay",0));
		
		if(percDone >= bouncePerc){
			//iTween.PunchScale(thisGO, {"amount": Vector3(.1,.1,0), "time":.7});
			iTween.PunchScale(thisGO, iTween.Hash ("amount", new Vector3(.025f,.025f,0), "time",.7f));
		}
		
	}
	
	//move the backgrounds so that it looks like it's dark where it's been
	private void moveBgs()
	{
		
		// print the rotation around the y-axis
		float angle = handT.eulerAngles.z;
		
		if(angle >= 180){//first half
			
			activeRightRenderer.sortingOrder = 5;
			inactiveHalfRenderer.sortingOrder = -5;
			activeR.transform.rotation = Quaternion.Euler(0, 0, angle);
			
		}else{//second half
			
			activeRightRenderer.sortingOrder = -5;
			inactiveHalfRenderer.sortingOrder = 6;
			inacHalf.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
			
		}
		
	}
	
	
	
	public void pauseTimer()
	{
		
		if(timerIsRunning)
			Time.timeScale = 0;
		
	}
	
	public void resumeTimer()
	{
		
		Time.timeScale = 1;
		
	}
	
	//stop the time and reset
	public void stopTimer()
	{

        if (timerIsRunning)
        {
            iTween.Stop(thisGO, true);
            resetGraphics();
            CancelInvoke("moveTime");
        }
		resetGraphics();
    }
	
	//overload method for stopTimer
	//only plays when it's complete
	public void stopTimer(bool isFinished)
	{
		
		if(timerIsRunning){
			iTween.Stop(thisGO, true);
			if(!isFinished)resetGraphics();
			CancelInvoke("moveTime");
		}
        if (this.tag == "Turn")
        {
            batonBan = baton;
            
        }

    }
	
	//set everything back to how it started
	private void resetGraphics()
	{
		
		activeRightRenderer.sortingOrder = 5;
		inactiveHalfRenderer.sortingOrder = -5;
		inacHalf.transform.eulerAngles = new Vector3(0,0,0);
		activeR.transform.eulerAngles = new Vector3(0,0,0);
		hand.transform.eulerAngles = new Vector3(0,0,359);

    }


    public void Update()
    {
        if (this.tag == "Turn") {
            if (timer15Started == true)
            {
                if (timerIsRunning == false)
                {
                    startTimer(secondsRemaining);
                }
                timer15Started = false;
            }
            if (baton == 1)
            {
                transform.Find("Background").GetComponent<SpriteRenderer>().color = new Color(0.778f, 0.778f, 0.778f);
            }else if (baton == 2)
            {
                transform.Find("Background").GetComponent<SpriteRenderer>().color = new Color(1, 1, 0.664f);
            }
        }
        else
        {
            if ((timerStarted==false||this.timeLeft>0)&&currentBoard != null && currentBoard.cursorExist == true)
            {
                
                if (isClicked == true)
                {
                    {
                        if ((baton == 1 && gameObject.tag == "Player1") || (baton == 2 && gameObject.tag == "Player2"))
                        {
                            Debug.Log(batonBan);
                            if (baton!=batonBan)
                            {
                                ringCnt = GameObject.FindGameObjectsWithTag("MoveAble");
                                GameObject obj;
                                if (ringCnt.Length == 0)
                                {
                                    GameObject self = gameObject;
                                    obj = Instantiate(range, self.transform.position, Quaternion.identity);
                                    obj.transform.parent = this.transform;
                                    obj.transform.localPosition = Vector3.zero;
                                    obj.transform.localRotation = Quaternion.identity;
                                    obj.transform.localScale = Vector3.one * 2;
                                }
                                else
                                {
                                    GameObject[] tempobj = GameObject.FindGameObjectsWithTag("MoveAble");
                                    foreach (GameObject ob in tempobj)
                                    {
                                        Destroy(ob);

                                        
                                    }
                                    GameObject self = gameObject;
                                    obj = Instantiate(range, self.transform.position, Quaternion.identity);
                                    obj.transform.parent = this.transform;
                                    obj.transform.localPosition = Vector3.zero;
                                    obj.transform.localRotation = Quaternion.identity;
                                    obj.transform.localScale = Vector3.one * 2;
                                }
                            }
                            else
                            {
                                GameObject[] tempobj = GameObject.FindGameObjectsWithTag("MoveAble");
                                foreach (GameObject ob in tempobj)
                                {
                                    Destroy(ob);
                                }
                                batonchange();
                            }
                            
                        }

                    }

                }
            }
        }
    }
}

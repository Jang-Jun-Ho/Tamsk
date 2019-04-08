#pragma strict

//this is how close to being done before it does the bounce animation (.8 means 80% done)
public var bouncePerc:float = .8;

//what would you like to do when the time runs out
public var sendMessageObject:GameObject;
public var sendMessage:String;

//start immediately when the scene runs
public var startOnPlay:boolean;
public var secondsRemaining:float = 16;

//define elements of the clocks\
public var hand:GameObject;
public var activeR:GameObject;
public var inacHalf:GameObject;
public var activeRightRenderer:Renderer;
public var inactiveHalfRenderer:Renderer;

//private vars
private var maxTime:float;
private var timeLeft:float;
private var thisGO:GameObject;
private var handT:Transform;
private var timerIsRunning:boolean;

function Start()
{
	
	thisGO = gameObject;
	handT = hand.transform;
	
	if(startOnPlay == true){
		startTimer(secondsRemaining);
	}
}

public function startTimer(timerSeconds:float):void 
{
	
	//make sure timescale is 1
	resumeTimer();

	maxTime = timerSeconds;
	timeLeft = timerSeconds;
	
	stopTimer();//make sure it's not already playing
	timerIsRunning = true;
	InvokeRepeating("moveTime", 1, 1);//start timer

}

//this function runs every second
private function moveTime()
{

	timeLeft = timeLeft - 1;
	
	determineAngle();

}

private function determineDone()
{
	
	//check if 0 seconds left
	if(timeLeft == 0){
		
		stopTimer(true);

		timerIsRunning = false;
	
		if(sendMessageObject){
		
			sendMessageObject.SendMessage(sendMessage);
			
		}
	}

}

//add seconds
public function addSeconds(timeToAdd:int):void
{
	
	if(timerIsRunning){
		timeLeft = timeLeft + timeToAdd;
		if(timeLeft > maxTime){
			timeLeft = maxTime;
		}
		determineAngle();
	}

}

//determine the angle of the hand
private function determineAngle()
{

	var percDone:float = 1 - timeLeft/maxTime;
	var angle:float = -percDone * 360;
	if(percDone <= 0)angle = 359;
	
	moveHands(percDone, angle);
	
}

//move the hands
private function moveHands(percDone:float, angle:float)
{

	iTween.RotateTo(hand,{"rotation":Vector3(0,0,angle),"time":.5,"delay":0, "onComplete":"determineDone", "onCompleteTarget":thisGO, "onUpdate":"moveBgs", "onUpdateTarget":thisGO});
	
	if(percDone >= bouncePerc){
		iTween.PunchScale(thisGO, {"amount": Vector3(.1,.1,0), "time":.7});
	}
	
}

//move the backgrounds so that it looks like it's dark where it's been
private function  moveBgs()
{

	// print the rotation around the y-axis
	var angle:float = handT.eulerAngles.z;
	
	if(angle >= 180){//first half
		
		//activeR.transform.localPosition.z = 4;
		activeRightRenderer.sortingOrder = 5;
		inactiveHalfRenderer.sortingOrder = -5;
		//inacHalf.transform.localPosition.z = 6;
		activeR.transform.rotation = Quaternion.Euler(0, 0, angle);
		
	}else{//second half
	
		//activeR.transform.localPosition.z = 6;
		//inacHalf.transform.localPosition.z = 3;
		activeRightRenderer.sortingOrder = -5;
		inactiveHalfRenderer.sortingOrder = 6;
		inacHalf.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
		
	}
	
}



public function pauseTimer():void
{
	
	if(timerIsRunning)
		Time.timeScale = 0;

}

public function resumeTimer():void
{
	
	Time.timeScale = 1;

}

//stop the time and reset
public function stopTimer():void
{
	
	if(timerIsRunning){
		iTween.Stop(thisGO, true);
		resetGraphics();
		CancelInvoke("moveTime");
	}
	
	resetGraphics();

}

//overload method for stopTimer
//only plays when it's complete
public function stopTimer(isFinished:boolean):void
{
	
	if(timerIsRunning){
		iTween.Stop(thisGO, true);
		if(!isFinished)resetGraphics();
		CancelInvoke("moveTime");
	}

}

//set everything back to how it started
private function resetGraphics():void
{
	
	activeRightRenderer.sortingOrder = 5;
	inactiveHalfRenderer.sortingOrder = -5;
	inacHalf.transform.eulerAngles.z = 0;
	activeR.transform.eulerAngles.z = 0;
	hand.transform.eulerAngles.z = 359;
	
}


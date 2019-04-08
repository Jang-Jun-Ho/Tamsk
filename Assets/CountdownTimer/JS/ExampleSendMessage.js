#pragma strict

public var timerScript:GameTimer;//define the game timer script. Go to the inspector to drag the Timer onto this var. 

function Start()
{
	
	timerScript.startTimer(8);//start the timer with 8 seconds
	timerScript.addSeconds(2);//add 2 seconds to the timer
	timerScript.pauseTimer();//pause time (note, this pauses time for the whole game!
	timerScript.resumeTimer();//resumes time
	timerScript.stopTimer();//stops and resets the timer
	
}


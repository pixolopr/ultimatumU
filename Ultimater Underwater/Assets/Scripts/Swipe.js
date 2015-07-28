#pragma strict

var TouchPos:Vector2;
var go:GameObject;
var lerp: float;
lerp = 0;
var take:int;
var newpos : Vector3;
var swipe = false;
var swipecondition = false;
var obj:GameObject;

var swipeleft = false;
var swiperight = false;

function Start () {
	newpos=go.transform.position;
	//take=PlayButton.score;
	/*obj=GameObject.Find("play");
	asd= persistentGameObject.GetComponent();*/
}

function Update () {
	//SWIPE FUNCTION//////////////
	if(swipe == true)
	{
		lerp = Time.deltaTime / 0.3f;
		go.transform.position = Vector3.Lerp(go.transform.position, newpos, lerp);
		//swipe = false;
		/*if(swiperight)
		{
			if(go.transform.position.x <= newpos.x )
			{	
				go.transform.Translate(5 * Time.deltaTime, 0 ,0);
			}else{
			 swipe = false;
			 swiperight = false;
			};
		}else{ if(swipeleft){
			if(go.transform.position.x >= newpos.x )
			{	
				go.transform.Translate(-5 * Time.deltaTime, 0 ,0);
			}else{
			 swipe = false;
			 swipeleft = false;
			};
		};
		};*/
	};
	
	/*if(swipecondition)
	{
		if(go.transform.position.x == newpos.x)
		{
				//Debug.Log("FALSE FALSE FALSE FALSE");
				swipe = false;
				swipecondition=false;
		};
	}else{
		if(go.transform.position.x == newpos.x)
		{
			
				//Debug.Log("FALSE FALSE FALSE FALSE");
				//Debug.Log(take);
				swipe = false;
				Debug.Log(swipe);
		};
	};*/
	
	//TESTING MOUSE CLICK SWIPE
	/*if(Input.GetMouseButtonDown(0)) {
		//newpos = Vector3(go.transform.position.x - 100f, go.transform.position.y, go.transform.position.z);
		//swipe = true;
		PlayButton.score = 1;
		take = PlayButton.score;
		Debug.Log(take);
		
	};
	if(Input.GetMouseButtonDown(1)) {
		//newpos = Vector3(go.transform.position.x + 100f, go.transform.position.y, go.transform.position.z);
		//swipe = true;
		//swipecondition = true;
		PlayButton.score = 2;
		take = PlayButton.score;
		Debug.Log(take);
	};*/

	//SWIPE DETECT FUNCTION
	if(Input.touchCount==1)
	{
		if(Input.GetTouch(0).phase==TouchPhase.Began)
		{
			TouchPos = Input.GetTouch(0).position;
		}
		if(Input.GetTouch(0).phase==TouchPhase.Ended)
		{
			//SWIPPE LOGIC
			if(Input.GetTouch(0).position.x > TouchPos.x+50)
			{
				Debug.Log("right");
				//CHECK end swipe	
				if(newpos.x < 11f){
					//SWIPE RIGHT
					Debug.Log("right");
					swiperight = true;
					newpos = Vector3(newpos.x + 10f, newpos.y, newpos.z);
					swipe = true;
					swipecondition=true;	
				};
			};
			if(Input.GetTouch(0).position.x<TouchPos.x-50)
			{
				
				if(newpos.x > -18f)
				{
				swipeleft = true;
					//SWIPE LEFT
					newpos = Vector3(go.transform.position.x - 10f, go.transform.position.y, go.transform.position.z);
			 		swipe = true;
				};
			};
		}
	}
}
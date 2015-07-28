using UnityEngine;
using System.Collections;

public class Swipe2 : MonoBehaviour {
	public GameObject thumb;
	Vector2 TouchPos;
	Vector3 newPos;
	bool swipe = false;
	bool swipecondition=false;
	float lerp=0;
	public int count=0;
	void Start () {
		newPos = thumb.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//WHEN SWIPE IS DONE
		if (swipe == true) {
			lerp=Time.deltaTime;
			thumb.transform.position=Vector3.Lerp(thumb.transform.position,newPos,lerp);
		}
		//SWIPE CONDITION
		if (swipecondition) {
			if (thumb.transform.position.x > newPos.x - 1) {
				swipe = false;
				swipecondition = false;
			}
		} else {
			if(thumb.transform.position.x<newPos.x+1)
			{
				swipe=false;
			}
		}
		//SWIPE DETECT FUNCTION
		if(Input.touchCount==1)
		{
			if(Input.GetTouch(0).phase==TouchPhase.Began)
			{
				TouchPos = Input.GetTouch(0).position;
			}
			if(Input.GetTouch(0).phase==TouchPhase.Ended)
			{
				if(count!=0)
				{
					if(Input.GetTouch(0).position.x>TouchPos.x+50)
					{
						if(thumb.transform.position.x>(newPos.x-1)){
						newPos =new Vector3(thumb.transform.position.x+11,thumb.transform.position.y,thumb.transform.position.z);
						swipe=true;
							swipecondition=true;
							Debug.Log("Right");
							count--;
						}
					}
				}
			if(count!=2)
			{
				if(Input.GetTouch(0).position.x<TouchPos.x-50)
				{
					if(thumb.transform.position.x < newPos.x +1)
					{
					newPos =new Vector3(thumb.transform.position.x-11,thumb.transform.position.y,thumb.transform.position.z);
					Debug.Log("Left");
					swipe=true;
					
					count++;
					}
				}
			  }
			}
		}
	}
}

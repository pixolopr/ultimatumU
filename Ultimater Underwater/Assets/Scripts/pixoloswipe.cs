using UnityEngine;
using System.Collections;

public class pixoloswipe : MonoBehaviour {

	Vector2 StartPos;
	int SwipeID = -1;
	float minMovement = 80.0f;

	public GameObject levels;

	float lerp;
	Vector3 newpos;

	public GameObject[] levelarray;
	int i;
	public static int level;

	bool swipe;

	// Use this for initialization
	void Start () {
		newpos = levels.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if(swipe == true){
			lerp = Time.deltaTime / 0.1f;
			Vector3 temp = new Vector3(-10.0f,0,0);
			levels.transform.position = Vector3.Lerp(levels.transform.position, newpos, lerp);
			if(levels.transform.position == newpos)
			{
				swipe = false;
			}
		};


		foreach (var T in Input.touches) {
			var P = T.position;
			if (T.phase == TouchPhase.Began && SwipeID == -1) {
				SwipeID = T.fingerId;
				StartPos = P;
			} else if (T.fingerId == SwipeID) {
				var delta = P - StartPos;
				if (T.phase == TouchPhase.Ended && delta.magnitude > minMovement) {
					SwipeID = -1;
					if (Mathf.Abs (delta.x) > Mathf.Abs (delta.y)) {
						if (delta.x > 0) {
							if(newpos.x < 10.0f){
								swipe = true;
								Vector3 temp = new Vector3(10.0f,0,0);
								newpos = newpos + temp;
							};
						} else {
							if(newpos.x > -18.0f)
							{
								swipe = true;
								Vector3 temp = new Vector3(-10.0f,0,0);
								newpos = newpos + temp;
							};
						}
					} else {
						if (delta.y > 0) {
							
							Debug.Log ("Swipe Up Found");
						} else {
							
							Debug.Log ("Swipe Down Found");
						}
					}
				} else if (T.phase == TouchPhase.Canceled || T.phase == TouchPhase.Ended)
				{
					SwipeID = -1;

					var ray = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
					RaycastHit2D hit = Physics2D.Raycast (ray, Vector2.zero);
					
					//COLLIDER TOUCHED
					if (hit.collider != null) {
						for(i=0; i<levelarray.Length; i++)
						{
							if(hit.collider.gameObject == levelarray[i])
							{
								level = i;
								Application.LoadLevel ("Game");
							};
						};

					};
				};
			}
		}
	
	}
}
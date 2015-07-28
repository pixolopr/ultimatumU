using UnityEngine;
using System.Collections;

public class levelselector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			
			if(Input.GetTouch(0).tapCount == 1)
			{
				Debug.Log("Tapped");
			};
		};
	}
}

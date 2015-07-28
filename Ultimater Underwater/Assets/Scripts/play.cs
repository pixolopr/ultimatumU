using UnityEngine;
using System.Collections;

public class play : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			var ray = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			/*var hit:RaycastHit;*/
			
			RaycastHit2D hit = Physics2D.Raycast (ray, Vector2.zero);
			
			if (hit.collider != null) {
				Debug.Log("hit");
				Application.LoadLevel ("Game");
			};
		};
	}
}

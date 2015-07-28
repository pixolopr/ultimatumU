using UnityEngine;
using System.Collections;

public class positiondata : MonoBehaviour {
	public static float[][] level1positions=new float[2][];
	// Use this for initialization
	void Start () {
		level1positions[0]=new float[10];
		level1positions[1]=new float[10];
		level1positions[0][0] = -7.01f;
		level1positions[1][0] = 5.26f;
		level1positions[0][1] = 1.4f;
		level1positions[1][1] = 5.65f;
		level1positions[0][2] = 3.64f;
		level1positions[1][2] = 1.91f;
		level1positions[0][3] = -0.75f;
		level1positions[1][3] = 2.52f;
		level1positions[0][4] = -5.65f;
		level1positions[1][4] = 2.49f;
		level1positions[0][5] = -3.21f;
		level1positions[1][5] = -1.33f;
		level1positions[0][6] = -3.35f;
		level1positions[1][6] = -4.088f;
		level1positions[0][7] = 1.29f;
		level1positions[1][7] = -1.24f;
		level1positions[0][8] = 5.15f;
		level1positions[1][8] = -2.32f;
		level1positions[0][9] = 2.04f;
		level1positions[1][9] = -4.64f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

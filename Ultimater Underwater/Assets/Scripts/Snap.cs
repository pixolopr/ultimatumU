using UnityEngine;
using System.Collections;

public class Snap : MonoBehaviour {

	int ag;


	public GameObject[] shadows;
	public GameObject[] animals;
	public GameObject[] boxes;
	public AudioClip[] sounds;
	public AudioClip[] animalsounds;
	public Animator[] animators;

	public AudioClip impact;
	AudioSource audio;

	GameObject detect;
	//GameObject detectsprite;
	GameObject detectshadow;
	GameObject detectbox;

	public GameObject popupbox;
	public GameObject popupanimal;

	GameObject upbox;

	public GameObject empty;
	Sprite anim;
	Sprite shad;

	bool[] boxanim = new bool[]{false,false,false,false,false,false,false,false,false,false};
	float[] boxpos = new float[]{-100f,-100f,-100f,-100f,-100f,-100f,-100f,-100f,-100f,-100f};

	//ARRAY OF LEVELS (CHANGE THIS WHEN LEVEL NUMBER INCREASES)
	string[][][] levels = new string[4][][];

	//VARIABLE TO STORE THE INITIAL POSITION OF THE ANIMAL
	Vector3 initposanimal = new Vector3();

	int i,j,l,k,detectnumber,m, count;

	//for creating a new pop up window
	Rect windowrect = new Rect(250,125,180,180);

	//for creating new button size
	Rect buttonrect = new Rect(40,145,100,23);
	bool popupcondition, gameend;
	public Texture2D popupimage;

	Texture2D[] animText = new Texture2D[10] ;


	void Start () {

		gameend = false;

		ag = 0;

		count = 0;

		detect = empty;
		detectshadow = empty;
 	
		for (int levelloop=0; levelloop<levels.Length; levelloop++) {

			//ASSIGN EMPTY 2-LEVEL ARRAY TO LEVELS ELEMENTS
			levels[levelloop] = new string[4][];

			//ASSIGN EMPTY 1-LEVEL ARRAY TO LEVELS [level] ELEMENTS
			levels[levelloop][0] = new string[10];
			levels[levelloop][1] = new string[10];
			levels[levelloop][2] = new string[10];
			levels[levelloop][3] = new string[10];

			for (int n= 1; n<animals.Length+1; n++) {
				int levelval = levelloop + 1;
				levels [levelloop] [0] [n - 1] = "level"+levelval+"/animal" + n;
				//Debug.Log(levels[levelloop][0][n-1]);
				levels [levelloop] [1] [n - 1] = "level"+levelval+"/shadow" + n;
				levels [levelloop] [2] [n - 1] = "level"+levelval+"/sound" + n;
				levels [levelloop] [3] [n - 1] = "level"+levelval+"/animalsound" + n;
			};
		};

		//LEVEL NUMBER
		l = pixoloswipe.level;

		audio = this.GetComponent<AudioSource>();

		//PLAY INTRO
		audio.clip = (AudioClip)Resources.Load("level"+(l+1)+"/intro");
		audio.Play();

		initposanimal = Vector3.zero;

		for (j=0; j<animals.Length; j++) {
			anim=Resources.Load<Sprite>(levels[l][0][j]);
			shad=Resources.Load<Sprite>(levels[l][1][j]);
			animText[j] = Resources.Load<Texture2D>(levels[l][0][j]);
			animals[j].GetComponent<SpriteRenderer>().sprite=anim;
			animals[j].transform.localScale = new Vector3(0.5f, 0.5f, 1);
			shadows[j].GetComponent<SpriteRenderer>().sprite=shad;
			//shadows[j].transform.position=new Vector3(positiondata.level1positions[0][j],positiondata.level1positions[1][j],0);

			sounds[j] =  (AudioClip)Resources.Load(levels[l][2][j]);
			animalsounds[j] = (AudioClip)Resources.Load(levels[l][3][j]);
		};
	}
	
	// Update is called once per frame
	void Update () {

		//LEVEL OVER - DTECT FINAL SOUND
		if (gameend) {
			if (!audio.isPlaying) {
				gameend = false;
				Application.LoadLevel ("levels");
			};
		};

		//TOUCH DETECTED
		if (Input.touchCount == 1) {

			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				var ray = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				RaycastHit2D hit = Physics2D.Raycast (ray, Vector2.zero);

				//COLLIDER TOUCHED
				if (hit.collider != null) {
					if(hit.collider.gameObject.name == "close")
					{
						givecolliders();
						popupbox.transform.position = new Vector3(-100, 0 , -1);
						popupanimal.GetComponent<Animator>().runtimeAnimatorController = null;
						if (count == 10) {
							//SOUNDS
							audio.clip = (AudioClip)Resources.Load("hurray");
							audio.Play();
							//SHOW GAME END POP-UP
							count = 0;
							gameend = true;
						};
					}else if(hit.collider.gameObject.name == "backButton"){
						Application.LoadLevel ("levels");
					}
					else{
						//LOOP THROUGH ALL ANIMALS
						for (i=0; i<animals.Length; i++) {
							//CHECK WHICH ANIMAL IS HIT
							if (animals[i] == hit.collider.gameObject) {
								//CHECK POSITION OF THAT ANIMAL TO SEE IF ITS TO BE SELECTED
								if(animals[i].transform.position != shadows[i].transform.position){
									//GIVE VALUE TO DETECT AND SHADOW
									detect = animals [i];
									detectshadow = shadows [i];
									detectbox = boxes[i];
									detectnumber = i;


									//SOUNDS
									audio.Stop();
									audio.clip = sounds[i];
									audio.Play();

									impact = animalsounds[i];

									if (initposanimal == Vector3.zero) {
										initposanimal = detect.transform.position;
									};
									//SCALE UP ANIMAL
									detect.transform.localScale = detectshadow.transform.localScale;
								};
							}else{
								animals[i].GetComponent<BoxCollider2D>().enabled = false;
							};
						};
					};
				};
			};
			//TOUCH MOVED
			if(Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				if(detect != empty)
				{
					Debug.Log(detect.transform.position);
					detect.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 5f));
				};
			};
			//TOUCH ENDED
			if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				if(detect != empty)
				{
					//CHECK POSITION OF DETECT AS COMPARED TO SHADOW
					if (detect.transform.position.x < detectshadow.transform.position.x + 1 && detect.transform.position.x > detectshadow.transform.position.x - 1) 
					{
						if (detect.transform.position.y < detectshadow.transform.position.y + 1 && detect.transform.position.y > detectshadow.transform.position.y - 1) 
						{
							count++;

							detect.transform.position = detectshadow.transform.position;

							popupcondition = true;

							detect.transform.parent = null;

							audio.Stop();
							audio.PlayOneShot (impact, 1f);

							initposanimal = Vector3.zero;
							detectbox.GetComponent<SpriteRenderer>().enabled=false;



							popupbox.transform.position = new Vector3(0, 0 , -1);
							popupanimal.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("level"+(l+1)+"/animalsprite"+(detectnumber+1)+"_0") as RuntimeAnimatorController;
							
							for(i=0; i<boxes.Length; i++)
							{
								if(i>detectnumber)
								{
									upbox = boxes[i];
									//boxes[i].transform.position = new Vector3(boxes[i].transform.position.x, boxes[i].transform.position.y + 2.47f, boxes[i].transform.position.z);
									//upbox.transform.Translate(0, 2.47f ,0 );
									boxanim[i] = true;
									boxpos[i] = upbox.transform.position.y + 2.47f;
								};
							};

						};
					};
					if (detect.transform.position != detectshadow.transform.position) {
						detect.transform.position = initposanimal;
						detect.transform.localScale = new Vector3(0.5f, 0.5f , 1);
						initposanimal = Vector3.zero;
					};
				};

				//THINGS THAT SHOULD HAPPEN IN ALL TOUCH ENDED

				if (!popupcondition)
				{
					givecolliders();
				};
				//MAKE DETECT AND SHADOW EMPTY
				detect = empty;
				detectshadow = empty;
			};
			
		};


		for(m=0; m<boxanim.Length; m++)
		{
			if(boxanim[m] == true)
			{
				if(boxpos[m] >= boxes[m].transform.position.y)
				{
					boxes[m].transform.Translate(0, 5 * Time.deltaTime ,0);
				}else{
					boxanim[m] = false;
					boxpos[m] = -100f;
				};
			};
		};



	}

	void givecolliders(){
		//GIVE BACK ALL OBJECTS THEIR COLLIDER
		for (i=0; i<animals.Length; i++)
		{
			popupcondition = false;
			animals[i].GetComponent<BoxCollider2D>().enabled = true;
		};
	}
}
	

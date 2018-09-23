using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Linq;
using Scorm;

public class GameManager : MonoBehaviour
{
	void Awake (){
		
	//IScormService scormService;

	//#if UNITY_EDITOR
	//scormService = new ScormPlayerPrefsService();
	//#else
	//scormService = new ScormService();
	//#endif

	

	//bool result = scormService.Initialize (Scorm.Version.Scorm_1_2); // Begins
	//communication with the LMS

	//if (result) {
		//scormService.SetMinScore (0); // Sets a min score of 0
		//scormService.SetMaxScore (300); // Sets a max score of 10
		//scormService.SetRawScore (6.5f); // Sets a score of 6.5
		//scormService.Commit (); // Commit the pending changes
		//scormService.Finish (); // Ends communication with the LMS
	//}
}



	SoundManager1 _soundManager;
	public SoundManager1 soundManager1
	{
		get
		{
			if (_soundManager == null)
				_soundManager = FindObjectOfType<SoundManager1>();

			return _soundManager;
		}
	}
	public GameObject brush;// brush
	private Color currentColor;	// The color that is currently selected on the brush
	private AudioSource audioSource;// audio source
	private Vector3 Postion = Vector3.zero;// mouse or touch postion
	public Text textScore;
    public Text totalScore;
	static int score1;
    public static int totalScore1;
    public static int painter;
	public static int count_try;
	public static int level;
	public Image timerBar;
	public float waitTime = 2.0f;
	public float turnDelay = 0.1f;
	bool coolingDown;
	public Image sound;
	public Sprite soundOn;
	public Sprite soundOff;
	public GameObject Correct;
	public GameObject Incorrect;



	// Create Array Filled with all the colors in the pallet
	Color[] color = new Color[20];

	// At the beginning of the game get the audio file and play it and add a red and green color to the color array
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		textScore.text = score1.ToString();
        totalScore.text = totalScore1.ToString();
		cMgrCommon.score_Color = 0;
		coolingDown = true;
		Correct.SetActive (false);
		Incorrect.SetActive (false);



		//cMGrCommon.level = 1;
		// my code
		color[0] = new Color((847085f/2097152f),(10000537f/16777216f),(476689f/40518561f),1f); //Green
		color[1] = new Color((14935013f/16777216f),(10395295f/33554432f),(19501048f/76504107f),1f); //red
		//color[1] = new Color(0.890,0.310,0.255,1); //red
        color[2] = new Color((1f), (623.5294f / 1000f), 0f, 1f); // Orange
        color[3] = new Color((0f), .6f, 1f, 1f); // Light Blue
        color[4] = new Color((.9019608f), .454902f, 454902f, 1f); // Pink
		color[5] = new Color32(133,119,181,255); // Purple
		color[6] = new Color32(79,193,93,255); // Light Green
		color[7] = new Color32(129,165,196,255); // Grayish-Blue
		color[8] = new Color32(0,171,164,255); // Turquoise
		color[9] = new Color32(54,68,144,255); // Dark Blue
		color[10] = new Color32(62,131,187,255); // Medium Blue
		color[11] = new Color32(221,147,87,255); // Peach
		color[12] = new Color32(101,148,59,255); // Forest Green
		color[13] = new Color32(139,87,34,255); // Brown
		color[14] = new Color32(103,11,121,255); // Violet
		color[15] = new Color32(231,181,0,255); // Dark Yellow
		color[16] = new Color32(235,0,102,255); // Dark Pink
		color[17] = new Color32(72,200,203,255); // Light Blue
		color[18] = new Color((0f), 0f, 0f, 0f); // Black
		color[19] = new Color32(239,57,19,255); //Orange
	
	}

	// Update the camera postion and then get the coordinates of the brush as it moves along the screen
	void Update ()
	{
		Postion = Camera.main.ScreenToWorldPoint (Input.mousePosition);// get click position
		Postion.z = -5;

		if (coolingDown == true) 
		{
			timerBar.fillAmount -= 1.0f / waitTime * Time.deltaTime;

			if (timerBar.fillAmount <= 0f) 
			{
				coolingDown = false;
				soundManager1.PlaySoundFaile();
				StartCoroutine(PauseROT(1f));
			}
		}
		//Correct.SetActive (false);
		//Incorrect.SetActive (false);

	// Returns whatever object is hit from RayCast
		if (Input.GetMouseButtonDown (0)) { 
			RayCast2D ();
			sceneEight ();
			sceneTwo ();
			sceneThree ();
			sceneFour ();
			sceneFive ();
			sceneSix ();
			sceneSeven ();
			sceneOne ();
		}
		

		if (brush != null)
		{
			brush.transform.position = Postion;//drag the brush
		}

	}
	IEnumerator Pause()
	{
		yield return new WaitForSeconds(3);
	}

	void pass()
	{
		cMGrCommon.Pass++;
	}


	bool checkColor(int brushCase)
	{
		if (brushCase == 0) {
			return true;
		} else {
			return false;
		}
	}


	//2d screen raycast
	private void RayCast2D ()
	{
		RaycastHit2D rayCastHit2D = Physics2D.Raycast (Postion, Vector2.zero);

	// If the paintbrush does not touch/return a paint color return nothing
		if (rayCastHit2D.collider == null) {
			return;
		}
        // If the paintbrush touches/returns a paint color assign the current color of the brush to the corresponding color
		if (rayCastHit2D.collider.tag == "BrushColor")
        {
            //set brush color
            currentColor = rayCastHit2D.collider.GetComponent<SpriteRenderer>().color;
            brush.transform.GetChild(0).GetComponent<SpriteRenderer>().color = currentColor;

			print (currentColor);

            if (currentColor.Equals(color[0]))
            {
                print("got Green");
            }
            if (currentColor.Equals(color[1]))
            {
                print("got Red");
            }
			if (currentColor.r == color[17].r)
			{
				print("True");
			}
			else
			{
				print("false");
			}
			if (currentColor.g == color[17].g)
			{
				print("True");
			}
			else
			{
				print("false");
			}
			if (currentColor.b == color[17].b)
			{
				print("True");
			}
			else
			{
				print("false");
			}


			//print (currentColor.r);
			//print (color [5].r);
			//print (currentColor.g);
			//print (color [5].g);
			//print (currentColor.b);
			//print (color[5].b * 10);
			//print (currentColor.a);
			//print (color [5].a);
			//print(currentColor.ToString());

        }

	}


	void checkScore(int Case)
	{

		switch (Case) {

		case 1:
			score1 += (100/3);
			break;
		case 2:
			score1 += 25;
			break;
        case 3:
            totalScore1 += 25;
            break;
        case 4:
            totalScore1 += 20;
            break;
		case 5:
			totalScore1 += 15;
			break;
		case 6:
			totalScore1 += 10;
			break;
		case 7:
			score1 += 50;
			break;
          

		}

        if(score1 == 99)
        {
            score1 = 100;
        }

        

		textScore.text = score1.ToString();
        totalScore.text = totalScore1.ToString();
		print (cMGrCommon.level);
		//print(cMGrCommon.countPaint);

    }

	bool used_all_Colors(bool[] used)
	{
		foreach(bool usedColor in used)
		{
			if (usedColor == false) 
			{
				return false;
			}
		}
		return true;
	}

    void sceneOne()
	{

		RaycastHit2D rayCastHit2D = Physics2D.Raycast(Postion, Vector2.zero);

		if (rayCastHit2D.collider == null || currentColor == color[18]) {
			return;
		}

		if (rayCastHit2D.collider.tag == "Camera 1" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			painter++;
			if (currentColor.Equals (color [1]) || (currentColor.Equals (color [2])) || (currentColor.Equals (color [19]))) {

				checkScore (1);
				soundManager1.PlaySoundScore ();
				if(currentColor.Equals(color[1]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[2]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[19]))
				{
					cMGrCommon.usedColor[2] = true;
				}
			} 
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}

		}
		else if (rayCastHit2D.collider.tag == "Camera 2" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			painter++;
			if(currentColor.Equals(color[2]) || (currentColor.Equals(color[1])) || (currentColor.Equals (color [19])))
            {

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[1]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[2]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[19]))
				{
					cMGrCommon.usedColor[2] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


		}
		else if (rayCastHit2D.collider.tag == "Camera 3" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			painter++;
			if(currentColor.Equals(color[1]) || (currentColor.Equals(color[2])) || (currentColor.Equals (color [19])))
            {

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[1]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[2]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[19]))
				{
					cMGrCommon.usedColor[2] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}
				
		}
			
		if (cMGrCommon.level == 0 && painter == 3 && (score1 == 100 && cMGrCommon.count_Try == 0) && used_all_Colors (cMGrCommon.usedColor) == true) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (4);
			//cMGrCommon.level+= 1;
			pass ();
			painter = 0;
			cMGrCommon.usedColor = new bool[3];
			Correct.SetActive (true);
			StartCoroutine (PauseRight (2f));
		} else if (cMGrCommon.level == 0 && painter == 3 && (score1 == 100 && cMGrCommon.count_Try == 1) && used_all_Colors (cMGrCommon.usedColor) == true) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (6);
			//cMGrCommon.level+= 1;
			pass ();   
			painter = 0;
			cMGrCommon.usedColor = new bool[3];
			Correct.SetActive (true);
			StartCoroutine (PauseRight (2f));
		} else if (cMGrCommon.level == 0 && painter == 3 && (score1 < 100 && cMGrCommon.count_Try == 1)) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			//cMGrCommon.level+=1;
			painter = 0;
			cMGrCommon.usedColor = new bool[3];
			Incorrect.SetActive (true);
			StartCoroutine (PauseRight (2f));
		} else if ((cMGrCommon.level == 0 && painter == 3 && (score1 < 100 && cMGrCommon.count_Try == 0))) {
			cMGrCommon.count_Try++;
			score1 = 0;
			painter = 0;
			cMGrCommon.usedColor = new bool[3];
			Incorrect.SetActive (true);
			StartCoroutine (PauseWrong (2f));
		} 
		else if(cMGrCommon.level == 0 && painter == 3 && used_all_Colors (cMGrCommon.usedColor) == false && cMGrCommon.count_Try == 0)
		{
			cMGrCommon.count_Try++;
			score1 = 0;
			painter = 0;
			Incorrect.SetActive (true);
			cMGrCommon.usedColor = new bool[3];
			StartCoroutine (PauseWrong (2f));
		}	
		else if(cMGrCommon.level == 0 && painter == 3 && used_all_Colors (cMGrCommon.usedColor) == false && cMGrCommon.count_Try == 1)
		{
			cMGrCommon.count_Try = 0;
			score1 = 0;
			//cMGrCommon.level+=1;
			painter = 0;
			cMGrCommon.usedColor = new bool[3];
			Incorrect.SetActive (true);
			StartCoroutine (PauseRight (2f));
		}	
			
	}

	void sceneTwo()
	{
		RaycastHit2D rayCastHit2D = Physics2D.Raycast(Postion, Vector2.zero);

		if (rayCastHit2D.collider == null || currentColor == color[18]) {
			return;
		}

		print (currentColor);

		if (rayCastHit2D.collider.tag == "Box 1" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			print ("hit1");
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[0]) || currentColor.Equals(color[1]))
            {

				checkScore(2);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[0]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[1]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				cMGrCommon.usedColor [2] = true;

			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


		}
		else if (rayCastHit2D.collider.tag == "Box 2" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			print ("hit2");
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			//count++;
			if(currentColor.Equals(color[0]) || currentColor.Equals(color[1]))
            {

				checkScore(2);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[0]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[1]))
				{
					cMGrCommon.usedColor[1] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


		}
		else if (rayCastHit2D.collider.tag == "Box 3" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			print ("hit3");
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			//count++;
			if(currentColor.Equals(color[1]) || currentColor.Equals(color[0]))
            {

				checkScore(2);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[0]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[1]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				print (cMGrCommon.usedColor [1]);
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


		}
		else if (rayCastHit2D.collider.tag == "Box 4" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			print ("hit4");
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			painter++;
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[1]) || currentColor.Equals(color[0]))
            {
				checkScore(2);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[0]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[1]))
				{
					cMGrCommon.usedColor[1] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}
				
		}
		if (cMGrCommon.level == 1 && cMGrCommon.countPaint == 4 && (score1 == 100 && cMGrCommon.count_Try == 0) && used_all_Colors (cMGrCommon.usedColor) == true) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (4);
			//cMGrCommon.level++;
			pass ();
			cMGrCommon.usedColor = new bool[3];
			cMGrCommon.countPaint = 0;
			Correct.SetActive (true);
			StartCoroutine(PauseRight(2f));
		} else if (cMGrCommon.countPaint == 4 && (score1 == 100 && cMGrCommon.count_Try == 1)&& cMGrCommon.level == 1 && used_all_Colors (cMGrCommon.usedColor) == true) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (6);
			//cMGrCommon.level++;
			pass ();
			cMGrCommon.usedColor = new bool[3];
			cMGrCommon.countPaint = 0;
			Correct.SetActive (true);
			StartCoroutine(PauseRight(2f));
		} else if (cMGrCommon.countPaint == 4 && (score1 < 100 && cMGrCommon.count_Try == 1)&& cMGrCommon.level == 1) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));
		} else if (cMGrCommon.countPaint == 4 && (score1 < 100 && cMGrCommon.count_Try == 0)&& cMGrCommon.level == 1) {
			cMGrCommon.count_Try++;
			score1 = 0;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseWrong(2f));
		}
		else if(cMGrCommon.level == 1 && cMGrCommon.countPaint == 4 && used_all_Colors (cMGrCommon.usedColor) == false && cMGrCommon.count_Try == 0)
		{
			cMGrCommon.count_Try++;
			score1 = 0;
			cMGrCommon.countPaint = 0;
			cMGrCommon.usedColor = new bool[3];
			Incorrect.SetActive (true);
			StartCoroutine(PauseWrong(2f));
		}	
		else if(cMGrCommon.level == 1 && cMGrCommon.countPaint == 4 && used_all_Colors (cMGrCommon.usedColor) == false && cMGrCommon.count_Try == 1)
		{
			cMGrCommon.count_Try = 0;
			score1 = 0;
			cMGrCommon.level+=1;
			cMGrCommon.countPaint = 0;
			cMGrCommon.usedColor = new bool[3];
			Incorrect.SetActive (true);
			StartCoroutine (PauseRight (2f));
		}	
    }

	void sceneThree()
	{
		int count = 0;

		RaycastHit2D rayCastHit2D = Physics2D.Raycast(Postion, Vector2.zero);

		if (rayCastHit2D.collider == null || currentColor == color[18]) {
			return;
		}

		if (rayCastHit2D.collider.tag == "House 1" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[1]) ||currentColor.Equals(color[3]) || currentColor.Equals(color[0]) ){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[0]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[1]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[3]))
				{
					cMGrCommon.usedColor[2] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}

			count++;

		}
		else if (rayCastHit2D.collider.tag == "House 2" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[3])|| currentColor.Equals(color[1]) || currentColor.Equals(color[0])){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[0]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[1]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[3]))
				{
					cMGrCommon.usedColor[2] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}

			count++;

		}
		else if (rayCastHit2D.collider.tag == "House 3" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{

			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[0]) || currentColor.Equals(color[1]) || currentColor.Equals(color[3])){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[0]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[1]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[3]))
				{
					cMGrCommon.usedColor[2] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}

			count++;
		}

		if (cMGrCommon.countPaint == 3 && (score1 == 100 && cMGrCommon.count_Try == 0)&& cMGrCommon.level == 2 && used_all_Colors (cMGrCommon.usedColor) == true) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (4);
			//cMGrCommon.level++;
			pass ();
			cMGrCommon.countPaint = 0;
			cMGrCommon.usedColor = new bool[3];
			Correct.SetActive (true);
			StartCoroutine(PauseRight(2f));
		} else if (cMGrCommon.countPaint == 3 && (score1 == 100 && cMGrCommon.count_Try == 1)&& cMGrCommon.level == 2 && used_all_Colors (cMGrCommon.usedColor) == true) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (6);
			//cMGrCommon.level++;
			pass ();
			cMGrCommon.countPaint = 0;
			cMGrCommon.usedColor = new bool[3];
			Correct.SetActive (true);
			StartCoroutine(PauseRight(2f));
		} else if (cMGrCommon.countPaint == 3 && (score1 < 100 && cMGrCommon.count_Try == 1)&& cMGrCommon.level == 2) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));
		} else if (cMGrCommon.countPaint == 3 && (score1 < 100 && cMGrCommon.count_Try == 0)&& cMGrCommon.level == 2) {
			cMGrCommon.count_Try++;
			score1 = 0;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseWrong(2f));
		}
		else if(cMGrCommon.level == 2 && cMGrCommon.countPaint == 3 && used_all_Colors (cMGrCommon.usedColor) == false && cMGrCommon.count_Try == 0)
		{
			cMGrCommon.count_Try++;
			score1 = 0;
			cMGrCommon.countPaint = 0;
			cMGrCommon.usedColor = new bool[3];
			Incorrect.SetActive (true);
			StartCoroutine(PauseWrong(2f));
		}	
		else if(cMGrCommon.level == 2 && cMGrCommon.countPaint == 3 && used_all_Colors (cMGrCommon.usedColor) == false && cMGrCommon.count_Try == 1)
		{
			cMGrCommon.count_Try = 0;
			score1 = 0;
			cMGrCommon.level+=1;
			cMGrCommon.countPaint = 0;
			cMGrCommon.usedColor = new bool[3];
			Incorrect.SetActive (true);
			StartCoroutine (PauseRight (2f));
		}	


	}

	void sceneFour()
	{
		int count = 0;

		RaycastHit2D rayCastHit2D = Physics2D.Raycast(Postion, Vector2.zero);

		if (rayCastHit2D.collider == null || currentColor == color[18]) {
			return;
		}

		if (rayCastHit2D.collider.tag == "Flower1" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[5]) || (currentColor.Equals(color[2])) || (currentColor.Equals(color[6]))){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[5]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[2]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[6]))
				{
					cMGrCommon.usedColor[2] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}

			count++;

		}
		else if (rayCastHit2D.collider.tag == "Flower 2" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[5]) || (currentColor.Equals(color[2])) || (currentColor.Equals(color[6]))){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[5]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[2]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[6]))
				{
					cMGrCommon.usedColor[2] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}

			count++;

		}
		else if (rayCastHit2D.collider.tag == "Flower 3" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{

			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[5]) || (currentColor.Equals(color[2])) || (currentColor.Equals(color[6]))){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[5]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[2]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[6]))
				{
					cMGrCommon.usedColor[2] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}

			count++;
		}

		if (cMGrCommon.countPaint == 3 && (score1 == 100 && cMGrCommon.count_Try == 0)&& cMGrCommon.level == 3 && used_all_Colors (cMGrCommon.usedColor) == true) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (4);
			//cMGrCommon.level++;
			pass ();
			cMGrCommon.countPaint = 0;
			//if (cMGrCommon.Pass > 1) {
				//cMGrCommon.Pass = 0;
				//SceneManager.LoadScene ("Color_Level2_Inst");
			Correct.SetActive (true);
			StartCoroutine(PauseRight(2f));
			//}
		} else if (cMGrCommon.countPaint == 3 && (score1 == 100 && cMGrCommon.count_Try == 1) && cMGrCommon.level == 3 && used_all_Colors (cMGrCommon.usedColor) == true) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (6);
			//cMGrCommon.level++;
			pass ();
			cMGrCommon.countPaint = 0;
			//if (cMGrCommon.Pass > 1) {
				//cMGrCommon.Pass = 0;
				//SceneManager.LoadScene ("Color_Level2_Inst");
			Correct.SetActive (true);
			StartCoroutine(PauseRight(2f));
			//}
		} else if (cMGrCommon.countPaint == 3 && (score1 < 100 && cMGrCommon.count_Try == 1)&& cMGrCommon.level == 3) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			//if (cMGrCommon.Pass > 1) {
				//cMGrCommon.Pass = 0;
				//SceneManager.LoadScene ("Color_Level2_Inst");
				Incorrect.SetActive (true);
				StartCoroutine(PauseRight(2f));
			//}
		} else if (cMGrCommon.countPaint == 3 && (score1 < 100 && cMGrCommon.count_Try == 0)&& cMGrCommon.level == 3) {
			cMGrCommon.count_Try++;
			score1 = 0;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseWrong(2f));
		}
		else if(cMGrCommon.level == 3 && cMGrCommon.countPaint == 3 && used_all_Colors (cMGrCommon.usedColor) == false && cMGrCommon.count_Try == 0)
		{
			cMGrCommon.count_Try++;
			score1 = 0;
			cMGrCommon.countPaint = 0;
			cMGrCommon.usedColor = new bool[3];
			Incorrect.SetActive (true);
			StartCoroutine(PauseWrong(2f));
		}	
		else if(cMGrCommon.level == 3 && cMGrCommon.countPaint == 3 && used_all_Colors (cMGrCommon.usedColor) == false && cMGrCommon.count_Try == 1)
		{
			cMGrCommon.count_Try = 0;
			score1 = 0;
			cMGrCommon.level+=1;
			cMGrCommon.countPaint = 0;
			cMGrCommon.usedColor = new bool[3];
			Incorrect.SetActive (true);
			StartCoroutine (PauseRight (2f));
		}	


	}


	void sceneFive()
	{
		int count = 0;

		RaycastHit2D rayCastHit2D = Physics2D.Raycast(Postion, Vector2.zero);

		if (rayCastHit2D.collider == null || currentColor == color[18]) {
			return;
		}


		if (rayCastHit2D.collider.tag == "Drink1" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[7]) || (currentColor.Equals(color[8])) || (currentColor.Equals(color[9]))){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[7]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[8]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[9]))
				{
					cMGrCommon.usedColor[3] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


			count++;

		}
		else if (rayCastHit2D.collider.tag == "Drink2" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[7]) || (currentColor.Equals(color[8])) || (currentColor.Equals(color[9]))){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[7]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[8]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[9]))
				{
					cMGrCommon.usedColor[3] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


			count++;

		}
		else if (rayCastHit2D.collider.tag == "Drink3" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{

			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[7]) || (currentColor.Equals(color[8])) || (currentColor.Equals(color[9]))){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[7]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[8]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[9]))
				{
					cMGrCommon.usedColor[3] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


			count++;
		}

		if (cMGrCommon.countPaint == 3 && (cMGrCommon.count_Try == 0 && score1 == 100) && cMGrCommon.level == 4 && used_all_Colors (cMGrCommon.usedColor) == true) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (4);
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Correct.SetActive (true);
			StartCoroutine(PauseRight(2f));
		}
		else if (cMGrCommon.countPaint == 3 && (cMGrCommon.count_Try == 0 && score1 == 66) && cMGrCommon.level == 4 && used_all_Colors (cMGrCommon.usedColor) == true) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (5);
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));
		}
		else if (cMGrCommon.countPaint == 3 && (cMGrCommon.count_Try == 0 && score1 == 33) && cMGrCommon.level == 4) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (6);
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));
		}
		else if (cMGrCommon.countPaint == 3 && (cMGrCommon.count_Try == 0 && score1 == 0) && cMGrCommon.level == 4) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));
		}


	}
	void sceneSix()
	{
		int count = 0;

		RaycastHit2D rayCastHit2D = Physics2D.Raycast(Postion, Vector2.zero);

		if (rayCastHit2D.collider == null || currentColor == color[18]) {
			return;
		}


		if (rayCastHit2D.collider.tag == "Car1" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[10]) || (currentColor.Equals(color[11]))) {

				checkScore(7);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[10]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[11]))
				{
					cMGrCommon.usedColor[1] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


			count++;

		}
		else if (rayCastHit2D.collider.tag == "Car2" && rayCastHit2D.collider.GetComponent<SpriteRenderer>().color == Color.white)
		{
			rayCastHit2D.collider.GetComponent<SpriteRenderer>().color = currentColor;
			audioSource.Play();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[10]) || (currentColor.Equals(color[11]))) {
				
				checkScore (7);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[10]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[11]))
				{
					cMGrCommon.usedColor[1] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


			count++;

		}
		if (cMGrCommon.countPaint == 2 && (cMGrCommon.count_Try == 0 && score1 == 100) && cMGrCommon.level == 5 && used_all_Colors (cMGrCommon.usedColor) == true) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (4);
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Correct.SetActive (true);
			StartCoroutine(PauseRight(2f));
		}
		else if (cMGrCommon.countPaint == 2 && (cMGrCommon.count_Try == 0 && score1 == 50) && cMGrCommon.level == 5) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (6);
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));
		}
		else if (cMGrCommon.countPaint == 2 && (cMGrCommon.count_Try == 0 && score1 == 0) && cMGrCommon.level == 5) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));
		}

	}
	void sceneSeven()
	{
		int count = 0;

		RaycastHit2D rayCastHit2D = Physics2D.Raycast (Postion, Vector2.zero);

		if (rayCastHit2D.collider == null || currentColor == color[18]) {
			return;
		}

		if (rayCastHit2D.collider.tag == "Table1" && rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color == Color.white) {
			rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color = currentColor;
			audioSource.Play ();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[12]) ||currentColor.Equals(color[13]) || currentColor.Equals(color[14]) ){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[12]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[13]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[14]))
				{
					cMGrCommon.usedColor[1] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


			count++;

		} else if (rayCastHit2D.collider.tag == "Table2" && rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color == Color.white) {
			rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color = currentColor;
			audioSource.Play ();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[12]) ||currentColor.Equals(color[13]) || currentColor.Equals(color[14]) ){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[12]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[13]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[14]))
				{
					cMGrCommon.usedColor[1] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


			count++;

		} else if (rayCastHit2D.collider.tag == "Table3" && rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color == Color.white) {

			rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color = currentColor;
			audioSource.Play ();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[12]) ||currentColor.Equals(color[13]) || currentColor.Equals(color[14]) ){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[12]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[13]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[14]))
				{
					cMGrCommon.usedColor[1] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


			count++;
		}

		if (cMGrCommon.countPaint == 3 && (cMGrCommon.count_Try == 0 && score1 == 100) && cMGrCommon.level == 6) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (4);
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Correct.SetActive (true);
			StartCoroutine(PauseRight(2f));
		} else if (cMGrCommon.countPaint == 3 && (cMGrCommon.count_Try == 0 && score1 == 66) && cMGrCommon.level == 6) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (5);
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));
		} else if (cMGrCommon.countPaint == 3 && (cMGrCommon.count_Try == 0 && score1 == 33) && cMGrCommon.level == 6) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (6);
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));
		} else if (cMGrCommon.countPaint == 3 && (cMGrCommon.count_Try == 0 && score1 == 0) && cMGrCommon.level == 6) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));

		}
	}

	void sceneEight()
	{
		int count = 0;
		//added
		Invoke( "Load", 10 );
		//


		RaycastHit2D rayCastHit2D = Physics2D.Raycast (Postion, Vector2.zero);

		if (rayCastHit2D.collider == null || currentColor == color[18]) {
			return;
		}


		if (rayCastHit2D.collider.tag == "Fish 1" && rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color == Color.white) {
			rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color = currentColor;
			audioSource.Play ();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[15]) || currentColor.Equals(color[16]) || currentColor.Equals(color[17])){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[15]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[16]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[17]))
				{
					cMGrCommon.usedColor[1] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


			count++;

		} else if (rayCastHit2D.collider.tag == "Fish 2" && rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color == Color.white) {
			rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color = currentColor;
			audioSource.Play ();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[15]) || currentColor.Equals(color[16]) || currentColor.Equals(color[17])){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[15]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[16]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[17]))
				{
					cMGrCommon.usedColor[1] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


			count++;

		} else if (rayCastHit2D.collider.tag == "Fish 3" && rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color == Color.white) {

			rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color = currentColor;
			audioSource.Play ();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[15]) || currentColor.Equals(color[16]) || currentColor.Equals(color[17])){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[15]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[16]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[17]))
				{
					cMGrCommon.usedColor[1] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}


			count++;
		} else if (rayCastHit2D.collider.tag == "Fish 5" && rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color == Color.white) {
			rayCastHit2D.collider.GetComponent<SpriteRenderer> ().color = currentColor;
			audioSource.Play ();
			cMGrCommon.countPaint++;
			if(currentColor.Equals(color[15]) || currentColor.Equals(color[16]) || currentColor.Equals(color[17])){

				checkScore(1);	
				soundManager1.PlaySoundScore();
				if(currentColor.Equals(color[15]))
				{
					cMGrCommon.usedColor[0] = true;
				}
				if(currentColor.Equals(color[16]))
				{
					cMGrCommon.usedColor[1] = true;
				}
				if(currentColor.Equals(color[17]))
				{
					cMGrCommon.usedColor[1] = true;
				}
			}
			else
			{
				print ("Wrong");
				soundManager1.PlaySoundFaile();
			}



			count++;

		}

		if (cMGrCommon.countPaint == 3 && (cMGrCommon.count_Try == 0 && score1 == 100) && cMGrCommon.level == 7) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (4);
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			//Application.LoadLevel("Color_Level3_Inst");
			SoundManager1.SetSoundOff();
			Correct.SetActive (true);
			StartCoroutine(PauseRight(2f));
		} else if (cMGrCommon.countPaint == 3 && (cMGrCommon.count_Try == 0 && score1 == 66) && cMGrCommon.level == 7) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (5);
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			SoundManager1.SetSoundOff();
			//Application.LoadLevel("Color_Level3_Inst");
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));
		} else if (cMGrCommon.countPaint == 3 && (cMGrCommon.count_Try == 0 && score1 == 33) && cMGrCommon.level == 7) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			checkScore (6);
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			SoundManager1.SetSoundOff();
			//Application.LoadLevel ("Color_Level3_Inst");
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));
		} else if (cMGrCommon.countPaint == 3 && (cMGrCommon.count_Try == 0 && score1 == 0) && cMGrCommon.level == 7) {
			cMGrCommon.count_Try = 0;
			score1 = 0;
			//cMGrCommon.level++;
			cMGrCommon.countPaint = 0;
			SoundManager1.SetSoundOff();
			//Application.LoadLevel("Color_Level3_Inst");
			Incorrect.SetActive (true);
			StartCoroutine(PauseRight(2f));

		}

	}	

	public void timeRunOut(int Level)
	{
		int fred = Level;
		switch (fred) 
		{
		 
		case 0:
			SceneManager.LoadScene ("Paint2");
			cMGrCommon.level++;
			score1 = 0;
			break;
		case 1:
			SceneManager.LoadScene ("Paint3");
			cMGrCommon.level++;
			score1 = 0;
			break;
		case 2:
			SceneManager.LoadScene ("Paint4");
			cMGrCommon.level++;
			score1 = 0;
			break;
			case 3:
			Application.LoadLevel("Color_Level2_Inst");
			cMGrCommon.level++;
			score1 = 0;
			break;
			case 4:
			SceneManager.LoadScene ("Paint6");
			cMGrCommon.level++;
			score1 = 0;
			break;
			case 5:
			SceneManager.LoadScene ("Paint7");
			cMGrCommon.level++;
			score1 = 0;
			break;
			case 6:
			SceneManager.LoadScene ("Paint8");
			cMGrCommon.level++;
			score1 = 0;
			break;
			case 7:
			Application.LoadLevel("Color_Level3_Inst");
			break;

		}

	}
	public void replayLevel(int Level)
	{
		int fred = Level;
		switch (fred) 
		{
		case 0:
			SceneManager.LoadScene ("Paint1");
			break;
		case 1:
			SceneManager.LoadScene ("Paint2");
			break;
		case 2:
			SceneManager.LoadScene ("Paint3");
			break;
		case 3:
			SceneManager.LoadScene ("Paint4");
			break;

		}
	}
	IEnumerator PauseROT(float pauseTime)
	{
		Debug.Log ("Inside PauseGame()");
		Time.timeScale = 0f;
		float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		Time.timeScale = 1f;
		Debug.Log("Done with my pause");
		Debug.Log(cMGrCommon.level);
		timeRunOut(cMGrCommon.level);

	}
	IEnumerator PauseWrong(float pauseTime)
	{
		Debug.Log ("Inside PauseGame()");
		Time.timeScale = 0f;
		float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		Time.timeScale = 1f;
		Debug.Log("Done with my pause");
		hide_Marks ();
		replayLevel (cMGrCommon.level);

	}
	IEnumerator PauseRight(float pauseTime)
	{
		Debug.Log ("Inside PauseGame()");
		Time.timeScale = 0f;
		float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		Time.timeScale = 1f;
		Debug.Log("Done with my pause");
		hide_Marks ();
		timeRunOut(cMGrCommon.level);

	}

	public void hide_Marks()
	{
		Incorrect.SetActive (false);
	}



}
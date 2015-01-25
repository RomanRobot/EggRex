using UnityEngine;
using System.Collections;

public class CutsceneScript : MonoBehaviour {
	public float duration = 5;
	public float currduration = 5;
	float image = 0;

	public GameObject image1;
	public GameObject image2;
	public GameObject activeImage;

	// Use this for initialization
	void Start ()
	{
		activeImage = image1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		activeImage.SetActive(true);
		currduration -= Time.deltaTime;

		if(currduration <= 0)
		{
			currduration = duration;
			if(image == 0)
			{
				image1.SetActive(false);
				activeImage = image2;
				image = 1;
			}
			else if (image == 1)
			{
				Application.LoadLevel("MainMenu");
				image2.SetActive(false);
				activeImage = image1;
				image = 0;
			}

		}
	}
}

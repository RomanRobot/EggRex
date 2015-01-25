using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	public float resetPoint = -574;
	public float beginPoint = 228;
	public float y;
	public float currentPoint;
	public float scrollVelocity;
	public GameObject mainMenu;
	public GameObject creditsMenu;

	// Update is called once per frame
	public void Update ()
	{
		Vector3 vec = Vector3.zero;
		vec.y = Time.deltaTime * scrollVelocity;
		transform.Translate(vec);

		if (transform.localPosition.y >= resetPoint)
		{
			BacktoMainMenu();
			//transform.position = new Vector3(136, -36, 95);
		}
	
	}

	public void BacktoMainMenu()
	{
		//Vector3 vec = Vector3.zero;
		//vec.y = -(transform.localPosition.y - beginPoint);
		//transform.Translate(vec);
		transform.localPosition = new Vector3(136, -36, 95);

		mainMenu.SetActive(true);
		creditsMenu.SetActive(false);
	}
}

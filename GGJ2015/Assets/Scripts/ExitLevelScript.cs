using UnityEngine;
using System.Collections;

public class ExitLevelScript : MonoBehaviour {

	public GameObject hero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if(collider.gameObject == hero)
		{
			Application.LoadLevel("EndScene");
		}
	}
}

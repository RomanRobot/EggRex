using UnityEngine;
using System.Collections;

public class UpgradeSpark : MonoBehaviour
{
	public float duration = 1.5f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		duration -= Time.deltaTime;

		if (duration <= 0.001f)
		{
			UnityEngine.Object.Destroy(this.gameObject);	
		}
	}
}

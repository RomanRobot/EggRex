using UnityEngine;
using System.Collections;

public class UpgradeStationScript : MonoBehaviour {

	public GameObject sparkEffect;
	public bool used = false;
	public GameObject hero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D( Collider2D collision )
	{
		if(collision.gameObject == hero && used == false)
		{
			GameObject spark;
			spark = (GameObject)Instantiate(sparkEffect);
			spark.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			spark.transform.Translate(0.03f, 0, 0);

			used = true;
		}
	}
}

using UnityEngine;
using System.Collections;

public class HamburgerWall : MonoBehaviour {

    public float duration = 5f;
    public float despawnDuration = 10f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D( Collider2D collider )
    {
        if (gameObject.collider2D.enabled == true && collider.gameObject.tag == "Player")
        {
            BoxCollider2D[] childColliders = GetComponentsInChildren<BoxCollider2D>();
            foreach (BoxCollider2D childCollider in childColliders)
            {
                childCollider.enabled = true;
                Destroy(childCollider.gameObject, Random.Range(duration, despawnDuration));
            }

            gameObject.collider2D.enabled = false;
        }
    }
}

using UnityEngine;
using System.Collections;

public class HamburgerWall : MonoBehaviour {

    public float duration = 5f;
    public float despawnDuration = 10f;

    bool hit = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D( Collider2D collider )
    {
        if (hit == false && collider.gameObject.tag == "Player")
        {
            BoxCollider2D[] childColliders = GetComponentsInChildren<BoxCollider2D>();
            foreach (BoxCollider2D childCollider in childColliders)
            {
                childCollider.enabled = true;
                Destroy(childCollider.gameObject, Random.Range(duration, despawnDuration));
            }

            hit = true;
        }
    }
}

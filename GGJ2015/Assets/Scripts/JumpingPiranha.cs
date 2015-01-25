using UnityEngine;
using System.Collections;

public class JumpingPiranha : MonoBehaviour {

    enum JPState
    {
        Jumping,
        Hidden
    }

    public float jumpHeight = 5f;

    public float jumpDuration = 1f;
    public float hideDuration = 1f;
    public float numberOfFlips = 1f;

    private Vector3 initialPosition;
    private SpriteRenderer spriteRenderer;
    private JPState state;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = JPState.Jumping;
	}
	
	// Update is called once per frame
	void Update () {
	    float loopTime = Time.time % (jumpDuration + hideDuration);
        if( loopTime < jumpDuration )
        {
            if(state != JPState.Jumping)
            {
                spriteRenderer.enabled = true;
                state = JPState.Jumping;
            }

            float t = loopTime / jumpDuration;
            float v = Mathf.Sin(Mathf.PI * t);

            // Set jump transform
            transform.position = initialPosition + new Vector3(0f, v * jumpHeight, 0f);
            //transform.rotation = Quaternion.Euler(0f, 0f, 270.0f + 180.0f * t * numberOfFlips); // Working
            transform.rotation = Quaternion.Euler(0f, 0f, 360.0f * v * numberOfFlips);
        }
        else
        {
            if (state != JPState.Hidden)
            {
                spriteRenderer.enabled = false;
                state = JPState.Hidden;
            }

            // Set hidden transform.
            transform.position = initialPosition;
        }
	}
}

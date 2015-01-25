using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[System.Serializable]
	public class StateTraits
	{
		public Animator animator;
		public GameObject animObject;
		public Collider2D collision;
	}

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
    [HideInInspector]
    public bool dash = false;
    public ParticleSystem jumpEffect;
    public Sprite landEffect;
	public Animator rexAnim;					// Reference to the player's Rex animator component.
	public Animator roboAnim;					// Reference to the player's Robo animator component.
    public bool isEgg = true;


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
    public float jumpForce = 1000f;			// Amount of force added when the player jumps.
    public float dashForce = 1000f;			// Amount of force added when the player dashes.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.


	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.

	//Animation objects
	public StateTraits eggTraits;
	public StateTraits rexTraits;
	public StateTraits roboTraits;
	public StateTraits currTraits;

	//Pickup objects
	public GameObject jumpCollectable;
	public GameObject dashCollectable;
	bool jumpEnabled = false;
	bool dashEnabled = false;

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		currTraits = eggTraits;
	}


	void Update()
	{
		if (eggTraits != currTraits)
		{
			// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
			grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
			currTraits.animator.SetBool("Grounded", grounded);
			// If the jump button is pressed and the player is grounded then the player should jump.
			if (Input.GetButtonDown("Jump") && grounded)
				jump = true;
			
		}
        if (isEgg == false)
        {
            if (Input.GetButtonDown("Dash"))
            {
                dash = true;
                gameObject.GetComponent<TrailRenderer>().enabled = true;
            }
        }
    }


	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		if (eggTraits != currTraits)
		{
			// The Speed animator parameter is set to the absolute value of the horizontal input.
			currTraits.animator.SetFloat("Speed", Mathf.Abs(h));			
		}
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

		if (eggTraits == currTraits)
		{
			if(h > 0)
			{
				transform.Rotate(Vector3.forward * -15);
			}
			else if (h < 0)
			{
				transform.Rotate(Vector3.forward * 15);
			}
		}
		else
		{
			// If the player should jump...
			if (jump && jumpEnabled)
			{
				// Set the Jump animator trigger parameter.
				//anim.SetTrigger("Jump");

				// Play a random jump audio clip.
				int i = Random.Range(0, jumpClips.Length);
				AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

				// Add a vertical force to the player.
				rigidbody2D.AddForce(new Vector2(0f, jumpForce));

				// Make sure the player can't jump again until the jump conditions from Update are satisfied.
				jump = false;
			}

			if (dash && dashEnabled)
			{
				Vector2 force = new Vector2(dashForce, 0f);

				if (!facingRight)
					force.x *= -1f;

				rigidbody2D.AddForce(force, ForceMode2D.Impulse);


				dash = false;
			}
		}
	}


	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


    void OnCollisionEnter2D( Collision2D collision )
    {
		if (eggTraits != currTraits)
		{
			if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
				Instantiate(landEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
			gameObject.GetComponent<TrailRenderer>().enabled = false;
		}
	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject == jumpCollectable && !jumpEnabled)
		{
			jumpEnabled = true;
			Evolve(EvolutionState.Jump);
		}
		if (collision.gameObject == dashCollectable && !dashEnabled)
		{
			Evolve(EvolutionState.Dash);
			dashEnabled = true;
		}
	}

	public enum EvolutionState { Egg, Jump, Dash}

	public void Evolve (EvolutionState state)
	{
		currTraits.animObject.SetActive(false);
		currTraits.collision.enabled = false;

        isEgg = false;
		//currAnim.SetActive(false);
		//currCollision.enabled = false;
		if (state == EvolutionState.Jump)
		{
			currTraits = rexTraits ;
			transform.rotation = Quaternion.Euler(Vector3.zero);

			transform.localScale = new Vector3(3, 3, 1);
			//transform.localPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
		}
		if (state == EvolutionState.Dash)
		{
			currTraits = roboTraits;
		}
		currTraits.collision.enabled = true;
		currTraits.animObject.SetActive(true);
	}
	//public IEnumerator Taunt()
	//{
	//	// Check the random chance of taunting.
	//	float tauntChance = Random.Range(0f, 100f);
	//	if(tauntChance > tauntProbability)
	//	{
	//		// Wait for tauntDelay number of seconds.
	//		yield return new WaitForSeconds(tauntDelay);
    //
	//		// If there is no clip currently playing.
	//		if(!audio.isPlaying)
	//		{
	//			// Choose a random, but different taunt.
	//			tauntIndex = TauntRandom();
    //
	//			// Play the new taunt.
	//			audio.clip = taunts[tauntIndex];
	//			audio.Play();
	//		}
	//	}
	//}


	//int TauntRandom()
	//{
	//	// Choose a random index of the taunts array.
	//	int i = Random.Range(0, taunts.Length);
    //
	//	// If it's the same as the previous taunt...
	//	if(i == tauntIndex)
	//		// ... try another random taunt.
	//		return TauntRandom();
	//	else
	//		// Otherwise return this index.
	//		return i;
	//}
}

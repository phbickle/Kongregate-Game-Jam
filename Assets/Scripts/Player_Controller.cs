using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour 
{
	public Vector2 playerVolocity;			//Varable used for adding external force to the player
	public float speed;						//Variable for adjusting force addeded to the ridged body for movement		
	public Rigidbody2D rb;					//Variable to hold the parmeters of the player ridged body

	// Use this for initialization
	void Start () 							//set initial values on load
	{
		rb = GetComponent<Rigidbody2D>();	
		playerVolocity.x = 0.0f;
		playerVolocity.y = 0.0f;

	}

	void FixedUpdate ()
	{
		ResetPlayerVolocity ();

		if (rb.velocity.x == 0.0f && rb.velocity.y == 0.0f) //only allow movement imputs if player is not moving. Will have to change once external factors are added
		{
			if (Input.GetAxis ("Horizontal") != 0.0f) 
			{
				SetPlayerVelocity (playerVolocity);
				rb.AddForce (playerVolocity * speed);
			} 
			else if (Input.GetAxis ("Vertical") != 0.0f) 
			{
				SetPlayerVelocity (playerVolocity);
				rb.AddForce (playerVolocity * speed);
			}
		}
	}

	void ResetPlayerVolocity()			//Function to reset the velocity of the player to 0
	{
		playerVolocity.x = 0.0f;
		playerVolocity.y = 0.0f;
	}

	void SetPlayerVelocity(Vector2 PlayerVelocity)	//Function to set the velocity of the player
	{
		playerVolocity.x = Input.GetAxisRaw ("Horizontal");
		playerVolocity.y = Input.GetAxisRaw ("Vertical");
	}
}

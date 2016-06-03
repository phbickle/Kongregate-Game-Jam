using UnityEngine;
using System.Collections;

public class GlobalTimers : MonoBehaviour 
{

	float counter;

	// Use this for initialization
	void Start ()
	{
		counter = 60.0f;

	}

	void FixedUpdate()
	{
		
	}

	void Update () 
	{
		counter = counter - Time.deltaTime;

		if(counter <= 0)
		{
			counter = 60.0f;
		}
	}

	void CheckScene()
	{
		return;
	}
}

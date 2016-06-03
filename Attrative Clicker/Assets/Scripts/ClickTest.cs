using UnityEngine;
using System.Collections;
using System.Collections.Generic;	

public class ClickTest : MonoBehaviour 
{
	GameObject monster;


	// Use this for initialization
	void Start () 
	{
		monster = GameObject.FindGameObjectWithTag("TestMonster");
	}
	
	// Update is called once per frame
	void Update () 
	{


	}

	void OnMouseDown()
	{
		Debug.Log ("clicked");
	}
}

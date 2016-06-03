using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectObject : MonoBehaviour 
{		
	public Inventory inventory;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnMouseDown()
	{
		inventory.GetComponent<Inventory> ().AddItem (2);
		Debug.Log ("Item Added");
		Destroy(gameObject);
	}

}

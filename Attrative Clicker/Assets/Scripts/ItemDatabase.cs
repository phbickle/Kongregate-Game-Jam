using UnityEngine;
using System.Collections;
using System.Collections.Generic;						//Gives acess to Lists

public class ItemDatabase : MonoBehaviour 
{
	public List<Items> items = new List<Items>();		//Initialised variable for list of Items

	void Start()
	{
		//Test Items
		//TODO: Initalise all items in list through a json or xml file.
		items.Add(new Items("Test Item 1", 0, "First Test Item", 1));
		items.Add(new Items("Test Item 2", 1, "Second Test Item", 2));
		//
	}
}

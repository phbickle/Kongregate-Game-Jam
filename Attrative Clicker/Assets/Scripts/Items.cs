using UnityEngine;
using System.Collections;

[System.Serializable]											//Allows access to set all variables in Unity frameword
public class Items 												//Item Class
{
	public string itemName;										//Variable to hold name of item
	public string itemDesc;										//Variable to hold description of item

	public int itemID;											//Variable to hold ID of item
	public int itemValue;										//Variable to hold Value of item

	public Texture2D itemIcon;									//Variable to hold Icon of item

	public Items(string name, int id, string desc, int value)	//Constructer for Item Class
	{
		itemName = name;
		itemDesc = desc;
		itemID = id;
		itemValue = value;
		itemIcon = Resources.Load<Texture2D> (name);
	}

	public Items()												//Defaul Constructer for Items Class
	{
	}
}

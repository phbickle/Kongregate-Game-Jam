using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

[System.Serializable]											//Allows access to set all variables in Unity frameword
public class Items 												//Item Class
{
	private string jsonString;
	private JsonData itemData;

	public string itemName;										//Variable to hold name of item
	public string itemDesc;										//Variable to hold description of item

	public int itemID;											//Variable to hold ID of item
	public int itemValue;										//Variable to hold Value of item

	public Texture2D itemIcon;									//Variable to hold Icon of item

	public Items(int id, string name, string desc, int value, string iconName)	//Constructer for Item Class
	{
		jsonString = File.ReadAllText (Application.dataPath + "/Resources/Items.json");
		itemData = JsonMapper.ToObject (jsonString);

		itemID = id;
		itemName = name;
		itemDesc = desc;
		itemValue = value;
		itemIcon = Resources.Load<Texture2D> (iconName);

	}

	JsonData GetItem(string name)
	{
		for(int i = 0; i < itemData["Items"].Count; i++)
		{
			if(itemData["Items"][i]["itemName"].ToString() == name)
			{
				return itemData["Items"][i];
			}
		}
		return null;
	}

	public Items()												//Defaul Constructer for Items Class
	{
		itemID = -1;
	}
}

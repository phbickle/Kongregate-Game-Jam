using UnityEngine;
using System.Collections;
using System.Collections.Generic;						//Gives acess to Lists
using System.IO;
using LitJson;

public class ItemDatabase : MonoBehaviour 
{
	public List<Items> items = new List<Items>();		//Initialised variable for list of Items

	private string jsonString;
	private JsonData itemData;

	void Start()
	{
		jsonString = File.ReadAllText (Application.dataPath + "/Resources/Items.json");
		itemData = JsonMapper.ToObject (jsonString);

		for (int i = 0; i < itemData ["Items"].Count; i++) 
		{
			items.Add (new Items (
				GetItemIDFromJson (itemData ["Items"] [i] ["itemID"].ToString ()), 
				GetItemNameFromJson (itemData ["Items"] [i] ["itemName"].ToString ()),
				GetItemDescFromJson (itemData ["Items"] [i] ["itemDesc"].ToString ()),
				GetItemValueFromJson (itemData ["Items"] [i] ["itemValue"].ToString ()),
				GetIconNameFromJson (itemData ["Items"] [i] ["iconName"].ToString ())
			));
		}
	}

	int GetItemIDFromJson(string id)
	{
		int idAsInt;

		for(int i = 0; i < itemData["Items"].Count; i++)
		{
			if((itemData["Items"][i]["itemID"].ToString()) == id)
			{
				idAsInt = int.Parse(itemData ["Items"] [i] ["itemID"].ToString());
				return idAsInt;
			}
		}
		return -1;
	}

	string GetItemNameFromJson(string name)
	{
		string nameAsString;

		for(int i = 0; i < itemData["Items"].Count; i++)
		{
			if((itemData["Items"][i]["itemName"].ToString()) == name)
			{
				nameAsString = itemData ["Items"] [i] ["itemName"].ToString();
				return nameAsString;
			}
		}
		return null;
	}

	string GetItemDescFromJson(string desc)
	{
		string descAsString;

		for(int i = 0; i < itemData["Items"].Count; i++)
		{
			if((itemData["Items"][i]["itemDesc"].ToString()) == desc)
			{
				descAsString = itemData ["Items"] [i] ["itemDesc"].ToString();
				return descAsString;
			}
		}
		return null;
	}

	int GetItemValueFromJson(string id)
	{
		int valueAsInt;

		for(int i = 0; i < itemData["Items"].Count; i++)
		{
			if((itemData["Items"][i]["itemValue"].ToString()) == id)
			{
				valueAsInt = int.Parse(itemData ["Items"] [i] ["itemValue"].ToString());
				return valueAsInt;
			}
		}
		return -1;
	}

	string GetIconNameFromJson(string iconName)
	{
		string iconNameAsString;

		for(int i = 0; i < itemData["Items"].Count; i++)
		{
			if((itemData["Items"][i]["iconName"].ToString()) == iconName)
			{
				iconNameAsString = itemData ["Items"] [i] ["iconName"].ToString();
				return iconNameAsString;
			}
		}
		return null;
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;						//Gives acess to Lists
using System.IO;
using LitJson;

public class WorldObjectDatabase : MonoBehaviour 
{
	public List<WorldObjects> objects = new List<WorldObjects>();		//Initialised variable for list of WorldObjects

	private string jsonString;
	private JsonData objectData;

	void Start()
	{
		jsonString = File.ReadAllText (Application.dataPath + "/Resources/WorldObjects.json");
		objectData = JsonMapper.ToObject (jsonString);

		for (int i = 0; i < objectData ["WorldObjects"].Count; i++) 
		{
			objects.Add (new WorldObjects (
				GetWorldObjectIDFromJson (objectData ["WorldObjects"] [i] ["objectID"].ToString ()), 
				GetWorldObjectNameFromJson (objectData ["WorldObjects"] [i] ["objectName"].ToString ()),
				GetIconNameFromJson (objectData ["WorldObjects"] [i] ["iconName"].ToString ())

			));
		}
	}

	int GetWorldObjectIDFromJson(string id)
	{
		int idAsInt;

		for(int i = 0; i < objectData["WorldObjects"].Count; i++)
		{
			if((objectData["WorldObjects"][i]["objectID"].ToString()) == id)
			{
				idAsInt = int.Parse(objectData ["WorldObjects"] [i] ["objectID"].ToString());
				return idAsInt;
			}
		}
		return -1;
	}

	string GetWorldObjectNameFromJson(string name)
	{
		string nameAsString;

		for(int i = 0; i < objectData["WorldObjects"].Count; i++)
		{
			if((objectData["WorldObjects"][i]["objectName"].ToString()) == name)
			{
				nameAsString = objectData ["WorldObjects"] [i] ["objectName"].ToString();
				return nameAsString;
			}
		}
		return null;
	}

	string GetIconNameFromJson(string iconName)
	{
		string iconNameAsString;

		for(int i = 0; i < objectData["WorldObjects"].Count; i++)
		{
			if((objectData["WorldObjects"][i]["iconName"].ToString()) == iconName)
			{
				iconNameAsString = objectData ["WorldObjects"] [i] ["iconName"].ToString();
				return iconNameAsString;
			}
		}
		return null;
	}



}

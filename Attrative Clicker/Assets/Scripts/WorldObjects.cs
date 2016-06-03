using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

[System.Serializable]											//Allows access to set all variables in Unity frameword
public class WorldObjects 												//object Class
{
	private string jsonString;
	private JsonData objectData;

	public string objectName;										//Variable to hold name of object

	public int objectID;											//Variable to hold ID of object

	public Texture2D objectIcon;									//Variable to hold Icon of object

	public WorldObjects(int id, string name, string iconName)	//Constructer for object Class
	{
		jsonString = File.ReadAllText (Application.dataPath + "/Resources/WorldObjects.json");
		objectData = JsonMapper.ToObject (jsonString);

		objectID = id;
		objectName = name;
		objectIcon = Resources.Load<Texture2D> (iconName);

	}

	JsonData Getobject(string name)
	{
		for(int i = 0; i < objectData["WorldObjects"].Count; i++)
		{
			if(objectData["WorldObjects"][i]["objectName"].ToString() == name)
			{
				return objectData["WorldObjects"][i];
			}
		}
		return null;
	}

	public WorldObjects()												//Defaul Constructer for WorldObjects Class
	{
		objectID = -1;
	}
}

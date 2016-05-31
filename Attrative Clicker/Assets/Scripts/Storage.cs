using UnityEngine;
using System.Collections;
using System.Collections.Generic;							//Give Access to Lists

public class Storage : MonoBehaviour 
{
	public int slotsX, slotsY;								//X and Y axis for Storage grid

	public GUISkin skin;									//Variable to hold background tile for Storage

	public List<Items> storage = new List<Items>();		//initalise a list of items. Used for adding items to Storage
	public List<Items> slots = new List<Items>();			//initalise a list of items. Used to store Storage with empty data 

	private ItemDatabase database;							//variable to hold an instance of the Item Database

	private bool showStorage;
	private bool showTooltip;								//bool variable for checking to show tooltip

	private string tooltip;									//string variable to hold tooltip information

	//Variables for drag and drop
	private bool draggingItem;
	private Items draggedItem;
	private int prevIndex;
	//

	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i < (slotsX * slotsY); i++)			//for loop that sets an empty item to each slot in the Storage
		{
			slots.Add(new Items());
			storage.Add (new Items ());
		}

		//Initialies database variable
		database = GameObject.FindGameObjectWithTag("StorageDatabase").GetComponent<ItemDatabase>();

		//Test of AddItem and RemoveItems Functions
		AddItem (0);
		AddItem (0);
		AddItem (1);
		RemoveItem (0);
		//

		//LoadStorage (); 								//TODO: remove commenting once test adds above are removed.


	}

	void Update()
	{
		if(Input.GetButtonDown("Storage"))				//show toggle for Storage on the GUI
		{
			showStorage = !showStorage;
		}
		if(Input.GetButtonDown("Inventory"))			//If the inventory is showing, hide it
		{
			if (showStorage == true)
			{
				showStorage = false;
			}
		}
	}

	void OnGUI()
	{
		tooltip = "";										//set the tooltip to blank every time OnGUI is called

		if (showStorage)									//Calls the DrawStorage function
		{
			DrawStorage ();

			if (showTooltip) 								//Draw the tooltip
			{
				GUI.Box (new Rect (Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 200, 200), tooltip, skin.GetStyle("button"));
			}

			if (draggingItem)								//Put the icon on the mouse cursor
			{
				GUI.DrawTexture(new Rect (Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
			}
		}



	}

	void DrawStorage()									//Function to draw the Storage on screen and set the proper icons to each Storage slot if not empty
	{
		Event e = Event.current;							//Variable to hold current event
		int i = 0;											//veriable to incriment when checking what to put in each Storage slot
		for(int y = 0; y < slotsY; y ++)					//Nested for loop to set each Storage slot
		{
			for (int x = 0; x < slotsX; x++)
			{
				Rect slotRect = new Rect (x * 68, y * 68, 64, 64);	//set dimentions of each space in the Storage

				GUI.Box (slotRect, y.ToString (), skin.GetStyle("skin"));	//draw first tile of Storage

				slots [i] = storage [i];					//set item at i in slots to the item held in the Storage at i

				if (slots [i].itemName != null) {				//Check to make sure slot should not be empty
					GUI.DrawTexture (slotRect, slots [i].itemIcon);	//Place icon for item in the proper spot in the Storage

					if (slotRect.Contains (e.mousePosition)) { //Check to see if mouse is over a slot in the Storage
						tooltip = CreateTooltip (slots [i]);			//Pass in current slot for tooltip

						showTooltip = true;

						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem) 
						{ //Drag items out of slots
							draggingItem = true;
							prevIndex = i;
							draggedItem = slots [i];
							storage [i] = new Items ();
						}
						if (e.type == EventType.mouseDown && draggingItem) 
						{ 	//Swap Items
							storage [prevIndex] = storage [i];
							storage [i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
							SaveStorage ();
						}
					}
				} 
				else 
				{
					if (slotRect.Contains (e.mousePosition)) 
					{ 				//Put dragged item into an empty slot
						if (e.type == EventType.mouseDown && draggingItem) 
						{
							storage [i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
							SaveStorage ();
						}

					}
				}

				if (tooltip == "") 							//if tooltip is emoty, do not show tooltip
				{
					showTooltip = false;
				}

				i++;									
			}
		}
	}

	string CreateTooltip(Items item)								//function to set the description shown on the tooltip
	{
		tooltip = item.itemName + "\n\n" + item.itemDesc;
		return tooltip;
	}

	void AddItem(int ID)											//Function to add items to the Storage
	{
		for(int i = 0; i < storage.Count; i++)					//loop through each item in the Storage
		{
			if(storage[i].itemName == null)						//check to see if the item at [i] in Storage has a name. If not, break out of loop. TODO Update logic for this check
			{
				for(int j = 0; j < database.items.Count; j++)		//Search items list for the item to be added
				{
					if(database.items[j].itemID == ID)
					{
						storage [i] = database.items [j];			//Replace current item (or add over empty item) at that slot in Storage with new item
					}
				}
				break;
			}
		}
	}

	void RemoveItem(int ID)											//Function to remove an item from the Storage. Currently removes the first instance item of that type in the Storage.
	{																//TODO Fix so that it removes the correct instance of the item instead of the first one
		for (int i = 0; i < storage.Count; i++) 					//loop through each item in the Storage
		{					
			if (storage [i].itemID == ID) 
			{
				storage [i] = new Items ();						//replace first item in the Storage that matches the ID, and replaces it with a blank item
				break;
			}

		}
	}

	bool StorageContains(int ID)									//bool function that returns if the Storage contains a specific item or not
	{
		bool result = false;
		for (int i = 0; i < storage.Count; i++) 
		{
			result = storage [i].itemID == ID;
			if (result) 
			{
				break;
			}
		}
		return result;
	}

	void SaveStorage()										//Save Storage functio
	{
		for (int i = 0; i < storage.Count; i++)
		{
			PlayerPrefs.SetInt("storage " + i, storage[i].itemID);
		}

	}

	void LoadStorage()										//Load Storage functin
	{
		for (int i = 0; i < storage.Count; i++) 
		{
			storage[i] = PlayerPrefs.GetInt("storage " + i, -1) >= 0 ? database.items[PlayerPrefs.GetInt("storage " + i)]  : new Items(); 
		}
	}
}

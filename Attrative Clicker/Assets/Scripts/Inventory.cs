using UnityEngine;
using System.Collections;
using System.Collections.Generic;							//Give Access to Lists

public class Inventory : MonoBehaviour 
{
	public int slotsX, slotsY;								//X and Y axis for inventory grid

	public GUISkin skin;									//Variable to hold background tile for inventory

	public List<Items> inventory = new List<Items>();		//initalise a list of items. Used for adding items to inventory
	public List<Items> slots = new List<Items>();			//initalise a list of items. Used to store inventory with empty data 

	private ItemDatabase database;							//variable to hold an instance of the Item Database

	private bool showInventory;
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
		for(int i = 0; i < (slotsX * slotsY); i++)			//for loop that sets an empty item to each slot in the inventory
		{
			slots.Add(new Items());
			inventory.Add (new Items ());
		}

		//Initialies database variable
		database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();

		//Test of AddItem and RemoveItems Functions
		AddItem (0);
		AddItem (0);
		AddItem (1);
		AddItem (2);
		RemoveItem (0);
		//

		//LoadInventory (); 								//TODO: remove commenting once test adds above are removed.


	}

	void Update()
	{
		if(Input.GetButtonDown("Inventory"))				//show toggle for inventory on the GUI
		{
			showInventory = !showInventory;
		}
		if(Input.GetButtonDown("Storage"))					//If the storage is showing, hide it
		{
			if (showInventory == true)
			{
				showInventory = false;
			}
		}
	}
	
	void OnGUI()
	{
		tooltip = "";										//set the tooltip to blank every time OnGUI is called

		if (showInventory)									//Calls the DrawInventory function
		{
			DrawInventory ();

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

	void DrawInventory()									//Function to draw the inventory on screen and set the proper icons to each inventory slot if not empty
	{
		Event e = Event.current;							//Variable to hold current event
		int i = 0;											//veriable to incriment when checking what to put in each inventory slot
		for(int y = 0; y < slotsY; y ++)					//Nested for loop to set each inventory slot
		{
			for (int x = 0; x < slotsX; x++)
			{
				Rect slotRect = new Rect (x * 68, y * 68, 64, 64);	//set dimentions of each space in the inventory

				GUI.Box (slotRect, y.ToString (), skin.GetStyle("skin"));	//draw first tile of inventory

				slots [i] = inventory [i];					//set item at i in slots to the item held in the inventory at i

				if (slots [i].itemName != null) {				//Check to make sure slot should not be empty
					GUI.DrawTexture (slotRect, slots [i].itemIcon);	//Place icon for item in the proper spot in the inventory

					if (slotRect.Contains (e.mousePosition)) { //Check to see if mouse is over a slot in the inventory
						tooltip = CreateTooltip (slots [i]);			//Pass in current slot for tooltip

						showTooltip = true;

						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem) 
						{ //Drag items out of slots
							draggingItem = true;
							prevIndex = i;
							draggedItem = slots [i];
							inventory [i] = new Items ();
						}
						if (e.type == EventType.mouseDown && draggingItem) 
						{ 	//Swap Items
							inventory [prevIndex] = inventory [i];
							inventory [i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
							SaveInventory ();
						}
					}
				} 
				else 
				{
					if (slotRect.Contains (e.mousePosition)) 
					{ 				//Put dragged item into an empty slot
						if (e.type == EventType.mouseDown && draggingItem) 
						{
							inventory [i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
							SaveInventory ();
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

	void AddItem(int ID)											//Function to add items to the inventory
	{
		for(int i = 0; i < inventory.Count; i++)					//loop through each item in the inventory
		{
			if(inventory[i].itemName == null)						//check to see if the item at [i] in inventory has a name. If not, break out of loop. TODO Update logic for this check
			{
				for(int j = 0; j < database.items.Count; j++)		//Search items list for the item to be added
				{
					if(database.items[j].itemID == ID)
					{
						inventory [i] = database.items [j];			//Replace current item (or add over empty item) at that slot in inventory with new item
					}
				}
				break;
			}
		}
	}

	void RemoveItem(int ID)											//Function to remove an item from the inventory. Currently removes the first instance item of that type in the inventory.
	{																//TODO Fix so that it removes the correct instance of the item instead of the first one
		for (int i = 0; i < inventory.Count; i++) 					//loop through each item in the inventory
		{					
			if (inventory [i].itemID == ID) 
			{
				inventory [i] = new Items ();						//replace first item in the inventory that matches the ID, and replaces it with a blank item
				break;
			}

		}
	}

	bool InventoryContains(int ID)									//bool function that returns if the inventory contains a specific item or not
	{
		bool result = false;
		for (int i = 0; i < inventory.Count; i++) 
		{
			result = inventory [i].itemID == ID;
			if (result) 
			{
				break;
			}
		}
		return result;
	}

	void SaveInventory()										//Save Inventory functio
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			PlayerPrefs.SetInt("inventory " + i, inventory[i].itemID);
		}

	}

	void LoadInventory()										//Load inventory functin
	{
		for (int i = 0; i < inventory.Count; i++) 
		{
			inventory[i] = PlayerPrefs.GetInt("inventory " + i, -1) >= 0 ? database.items[PlayerPrefs.GetInt("inventory " + i)]  : new Items(); 
		}
	}
}

using UnityEngine;
using System.Collections;

public class onDeath : MonoBehaviour {
	
	//public GameObject[] droppableItems;
	//public GameObject[] droppableItemsClone;
	public Transform monster;
	public Transform coin;


	int health = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown () {
		health -= 1;
		if (health == 0) {
			//gameObject.active = false;//makes the enemy "disappear", should look into how to actually remove it so its not taking up any resources.
			Destroy(gameObject);
			//could make the looting mechanism here
			//droppableItemsClone[0] = Instantiate (droppableItems[0], monster.transform.position, Quaternion.identity) as GameObject;
			Instantiate ( coin, monster.transform.position, Quaternion.identity);
				}

			}
}

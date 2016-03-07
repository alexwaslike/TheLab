using UnityEngine;
using System.Collections.Generic;

public class LevelGeneration : MonoBehaviour {

	public GameController GameController;
	public PrefabController PrefabController;
	public Transform TileParent;
	public Transform EnvironmentParent;
	public Transform DogParent;
	public Transform MonsterParent;
	public Transform ItemParent;
	public Transform PlayerLoc;

	private Vector3[] Grid;

	private GameObject[] Tiles;
	private int _tiles_max_X = 100;
	private int _tiles_max_Y = 50;
	private GameObject[] EnvironmentObjs;
	private GameObject[] Dogs;
	private GameObject[] Monsters;
	private GameObject[] Items;

	public int xOffset;
	public int yOffset;
	public float radius;
	public int MaxChance;
	public int EnvironmentObjChance;
	public int DogChance;
	public int MonsterChance;
	public int ItemChance;


	void Awake(){

		Grid =				new Vector3[_tiles_max_X*_tiles_max_Y];
		Tiles =				new GameObject[_tiles_max_X*_tiles_max_Y];
		EnvironmentObjs =	new GameObject[_tiles_max_X*_tiles_max_Y];
		Dogs =				new GameObject[_tiles_max_X*_tiles_max_Y];
		Monsters =			new GameObject[_tiles_max_X*_tiles_max_Y];
		Items =				new GameObject[_tiles_max_X*_tiles_max_Y];

		GenerateTiles ();
		GenerateEnvironmentObjects ();
		GenerateDogs ();
		GenerateMonsters();
		GenerateItems ();

	}

	void Update(){

		for (int i = 0; i < Grid.Length; i++) {

			Vector3 position = Grid [i];

			if (Mathf.Abs (position.x - PlayerLoc.position.x) >= radius || Mathf.Abs (position.y - PlayerLoc.position.y) >= radius) {
				Tiles [i].SetActive (false);
				if(EnvironmentObjs[i] != null)
					EnvironmentObjs [i].SetActive (false);
				if(Items[i] != null)
					Items [i].SetActive (false);
			} else {
				Tiles [i].SetActive (true);
				if(EnvironmentObjs[i] != null)
					EnvironmentObjs [i].SetActive (true);
				if(Items[i] != null)
					Items [i].SetActive (true);
			}
		}

		for (int i = 0; i < Monsters.Length; i++) {

			if (Monsters [i] != null) {
				Vector3 position = Monsters [i].transform.position;
				if ((Mathf.Abs (position.x - PlayerLoc.position.x) >= radius || Mathf.Abs (position.y - PlayerLoc.position.y) >= radius)) {
					Monsters [i].SetActive (false);
				} else
					Monsters [i].SetActive (true);
			}
		}

		for (int i = 0; i < Dogs.Length; i++) {

			if (Dogs [i] != null) {
				Vector3 position = Dogs [i].transform.position;
				if ((Mathf.Abs (position.x - PlayerLoc.position.x) >= radius || Mathf.Abs (position.y - PlayerLoc.position.y) >= radius)) {
					Dogs [i].SetActive (false);
				} else if(Dogs[i].GetComponent<Creature>().CurrentState != State.InInventory)
					Dogs [i].SetActive (true);
			}
		}
		

	}

	private void GenerateTiles(){

		GameObject tilePrefab = GameController.PrefabController.Tiles[0];

		float xSize = 3.61f;
		float ySize = 1.82f;
		int i = 0;
		for (int x = 0 + xOffset; x < _tiles_max_X + xOffset; x++) {
			for(int y = 0 + yOffset; y < _tiles_max_Y + yOffset; y++){

				if (i < Tiles.Length) {

					Vector3 newPos = new Vector3 (x * xSize, y * ySize, 0);
					Vector3 newPos2 = new Vector3 (x * xSize + xSize / 2, y * ySize - ySize / 2, 0);

					Grid [i] = newPos;
					Grid [i + 1] = newPos2;

					Tiles[i] = Instantiate (tilePrefab, newPos, Quaternion.identity) as GameObject;
					Tiles[i].transform.SetParent (TileParent, true);
					Tiles[i + 1] = Instantiate (tilePrefab, newPos2, Quaternion.identity) as GameObject;
					Tiles[i + 1].transform.SetParent (TileParent, true);

				} else
					break;

				i += 2;
			}
		}
	}

	private void GenerateEnvironmentObjects(){
		
		int objRoll = 0;
		int i = 0;
		for (int x = 0 + xOffset; x < _tiles_max_X + xOffset; x++) {
			for(int y = 0 + yOffset; y < _tiles_max_Y + yOffset; y++){

				objRoll = Random.Range (0, MaxChance);
				if (i < Grid.Length) {

					if (objRoll <= EnvironmentObjChance) {

						objRoll = Random.Range (0, PrefabController.Environment.Count);
						EnvironmentObjs [i] = Instantiate (PrefabController.Environment[objRoll], Grid [i], Quaternion.identity) as GameObject;
						EnvironmentObjs [i].transform.SetParent (EnvironmentParent, true);
						EnvironmentObjs [i].GetComponent<EnvironmentObject> ().GameController = GameController;

					}

				} else
					break;

				i += 2;
			}
		}
	}

	private void GenerateDogs(){
		
		int objRoll = 0;
		int i = 0;
		for (int x = 0 + xOffset; x < _tiles_max_X + xOffset; x++) {
			for(int y = 0 + yOffset; y < _tiles_max_Y + yOffset; y++){

				objRoll = Random.Range (0, MaxChance);
				if (i < Grid.Length) {

					if (objRoll <= DogChance) {

						objRoll = Random.Range (0, PrefabController.Dogs.Count);
						Dogs [i] = Instantiate (PrefabController.Dogs[objRoll], Grid [i], Quaternion.identity) as GameObject;
						Dogs [i].transform.SetParent (DogParent, true);
						Dogs [i].GetComponent<Creature> ().GameController = GameController;
						Dogs [i].name = "Dog " + i;

					}

				} else
					break;

				i += 2;
			}
		}
	}

	private void GenerateMonsters(){
		
		int objRoll = 0;
		int i = 0;
		for (int x = 0 + xOffset; x < _tiles_max_X + xOffset; x++) {
			for(int y = 0 + yOffset; y < _tiles_max_Y + yOffset; y++){

				objRoll = Random.Range (0, MaxChance);
				if (i < Grid.Length) {

					if (objRoll <= MonsterChance) {

						objRoll = Random.Range (0, PrefabController.Monsters.Count);
						Monsters [i] = Instantiate (PrefabController.Monsters[objRoll], Grid [i], Quaternion.identity) as GameObject;
						Monsters [i].transform.SetParent (MonsterParent, true);
						Monsters [i].GetComponent<Creature> ().GameController = GameController;

					}

				} else
					break;

				i += 2;
			}
		}

	}

	private void GenerateItems(){
		
		int objRoll = 0;
		int i = 0;
		for (int x = 0 + xOffset; x < _tiles_max_X + xOffset; x++) {
			for(int y = 0 + yOffset; y < _tiles_max_Y + yOffset; y++){

				objRoll = Random.Range (0, MaxChance);
				if (i < Grid.Length) {

					if (objRoll <= ItemChance) {

						objRoll = Random.Range (0, PrefabController.Items.Count);
						Items [i] = Instantiate (PrefabController.Items[objRoll], Grid [i], Quaternion.identity) as GameObject;
						Items [i].transform.SetParent (ItemParent, true);
						Items [i].GetComponent<Item> ().GameController = GameController;

					}

				} else
					break;

				i += 2;
			}
		}

	}



}

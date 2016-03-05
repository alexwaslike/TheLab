using UnityEngine;
using System.Collections.Generic;

public class LevelGeneration : MonoBehaviour {

	public GameController GameController;
	public Transform TileParent;
	public Transform PlayerLoc;

	public GameObject[] LevelGrid;
	private int _max_X = 100; //100
	private int _max_Y = 50; //50

	private GameObject tilePrefab;

	public int xOffset;
	public int yOffset;
	public float radius;


	void Start(){

		tilePrefab = GameController.PrefabController.prefab_GrassTile;
		LevelGrid = new GameObject[_max_X*_max_Y];

        float xSize = 3.61f;
        float ySize = 1.82f;

		int i = 0;
		for (int x = 0 + xOffset; x < _max_X + xOffset; x++) {
			
			for(int y = 0 + yOffset; y < _max_Y + yOffset; y++){
				
				if (i < LevelGrid.Length) {
					Vector3 newPos = new Vector3 (x * xSize, y * ySize, 0);
					Vector3 newPos2 = new Vector3 (x * xSize + xSize / 2, y * ySize - ySize / 2, 0);

					LevelGrid[i] = Instantiate (tilePrefab, newPos, Quaternion.identity) as GameObject;
					LevelGrid[i].transform.SetParent (TileParent, true);
					LevelGrid[i + 1] = Instantiate (tilePrefab, newPos2, Quaternion.identity) as GameObject;
					LevelGrid[i + 1].transform.SetParent (TileParent, true);

				} else
					break;

				i += 2;

            }

		}

	}

	void Update(){

		for (int i = 0; i < LevelGrid.Length; i++) {

			Vector3 position = LevelGrid [i].transform.position;

			if ( Mathf.Abs(position.x - PlayerLoc.position.x) >= radius || Mathf.Abs(position.y - PlayerLoc.position.y) > radius ) {
				LevelGrid [i].SetActive (false);
			} else
				LevelGrid [i].SetActive (true);

		}
		

	}



}

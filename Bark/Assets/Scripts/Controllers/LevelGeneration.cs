using UnityEngine;
using System.Collections.Generic;

public class LevelGeneration : MonoBehaviour {

	public GameController GameController;

	public List<Vector3> LevelGrid;
	private float _max_X = 100;
	private float _max_Y = 100;
	private GameObject tilePrefab;

	void Start(){

		tilePrefab = GameController.PrefabController.prefab_GrassTile;

		LevelGrid = new List<Vector3> ();

		for (int x = 0; x < _max_X; x++) {
			for(int y = 0; y <_max_Y; y++){
				Vector3 newPos = new Vector3 (x, y, 0);
				LevelGrid.Add (newPos);
				Instantiate (tilePrefab, newPos + new Vector3(tilePrefab.transform.lossyScale.x/2, tilePrefab.transform.lossyScale.y/2, 0), Quaternion.identity);
			}
		}

	}



}

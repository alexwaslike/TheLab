using UnityEngine;
using System.Collections.Generic;

public class LevelGeneration : MonoBehaviour {

	public GameController GameController;

	public List<Vector3> LevelGrid;
	private float _max_X = 100;
	private float _max_Y = 10;
	private GameObject tilePrefab;

	void Start(){

		tilePrefab = GameController.PrefabController.prefab_GrassTile;

		LevelGrid = new List<Vector3> ();

        float xSize = 3.61f;
        float ySize = 1.82f;

        for (int x = 0; x < _max_X; x++) {
            for(int y = 0; y <_max_Y; y++){
				Vector3 newPos = new Vector3 (x, y, 0);
				//LevelGrid.Add (newPos);
				Instantiate (tilePrefab, new Vector3(newPos.x*xSize, newPos.y*ySize, 0), Quaternion.identity);
                Instantiate(tilePrefab, new Vector3(newPos.x * xSize + xSize/2, newPos.y * ySize - ySize/2, 0), Quaternion.identity);
            }
		}

	}



}

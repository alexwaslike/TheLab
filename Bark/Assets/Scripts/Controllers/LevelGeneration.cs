using UnityEngine;

public class LevelGeneration : MonoBehaviour {

	public GameController GameController;
	public PrefabController PrefabController;
	public Transform TileParent;
	public Transform EnvironmentParent;
	public Transform DogParent;
	public Transform MonsterParent;
	public Transform ItemParent;
	public Transform PlayerLoc;

	private Vector3[,] Grid;
	private int _max_X = 100;
	private int _max_Y = 50;

	private GameObject[,] Objects;
    private GameObject[,] Overlays;

    public int TreeClusterMin = 0;
    public int TreeClusterMax = 5;
    public int PlantClusterMin = 0;
    public int PlantClusterMax = 10;
    public int RockClusterMin = 1;
    public int RockClusterMax = 5;

    private int xPos;
    private int yPos;
    public int distanceMin = -1;
    public int distanceMax = 1;

	public float FoliageClusterChance = 10f;
	public float BuildingChance = 2f;
	public float DogChance = 5f;
	public float MonsterChance = 5f;
    public float OverlayChance = 2f;
    public float ItemChance = 1f;

	private GameObject[,] Tiles;
    public GameObject tilePrefab;

	public int xOffset;
	public int yOffset;
	public float radiusFromPlayer;

    private float _nearClipPlane;

	void Awake(){

        _nearClipPlane = GameController.MainCamera.nearClipPlane;

        tilePrefab = GameController.PrefabController.Tiles[0];

        Grid =		new Vector3[_max_X, _max_Y];
		Tiles =		new GameObject[_max_X, _max_Y];
		Objects =	new GameObject[_max_X, _max_Y];
        Overlays =  new GameObject[_max_X, _max_Y];

		GenerateTiles();
		GenerateEnvironment();
        GenerateMonsters();
        GenerateDogs();
        GenerateOverlays();
        GenerateItems();
    }

	void Update(){

        Vector3 cameraBottomLeft = GameController.MainCamera.ViewportToWorldPoint(new Vector3(0, 0, _nearClipPlane));
        Vector3 cameraTopRight = GameController.MainCamera.ViewportToWorldPoint(new Vector3(1, 1, _nearClipPlane));

        Vector3 position;
        for(int x = 0; x < _max_X; x++)
        {
            for(int y = 0; y < _max_Y; y++)
            {

                if(Tiles[x,y] != null) {
                    position = Grid[x, y];
                    if(position.x > cameraTopRight.x + 2 || position.x < cameraBottomLeft.x - 2 || position.y > cameraTopRight.y + 1 || position.y < cameraBottomLeft.y - 1) {
                        Tiles[x, y].SetActive(false);
                    } else if (position.x - cameraTopRight.x < 2 || position.x - cameraBottomLeft.x < -2 || position.y - cameraTopRight.y < 1 || position.y - cameraBottomLeft.y < -1) {
                        Tiles[x, y].SetActive(true);
                    }
                }

                if (Objects[x, y] != null) {
                    position = Objects[x, y].transform.position;
                    if (position.x > cameraTopRight.x + 5 || position.x < cameraBottomLeft.x - 5 || position.y > cameraTopRight.y + 5 || position.y < cameraBottomLeft.y - 5) {
                        Objects[x, y].SetActive(false);
                    } else if (position.x - cameraTopRight.x < 5 || position.x - cameraBottomLeft.x < -5 || position.y - cameraTopRight.y < 5 || position.y - cameraBottomLeft.y < -5) {
                        Objects[x, y].SetActive(true);
                    }
                }

                if (Overlays[x, y] != null) {
                    position = Overlays[x, y].transform.position;
                    if (position.x > cameraTopRight.x + 5 || position.x < cameraBottomLeft.x - 5 || position.y > cameraTopRight.y + 5 || position.y < cameraBottomLeft.y - 5) {
                        Overlays[x, y].SetActive(false);
                    } else if (position.x - cameraTopRight.x < 5 || position.x - cameraBottomLeft.x < -5 || position.y - cameraTopRight.y < 5 || position.y - cameraBottomLeft.y < -5) {
                        Overlays[x, y].SetActive(true);
                    }
                }
            }
        }

    }

    private void GenerateTiles(){
        
		float xSize = 3.61f;
		float ySize = 1.82f;
        bool alt = false;

        for (int x = 0; x < _max_X; x++) {
			for (int y = 0; y < _max_Y; y++){

                Vector3 newPos = Vector3.zero;

                if(alt)
                    newPos = new Vector3(x * (xSize / 2), y * ySize + ySize/2, 0);
                else
                    newPos = new Vector3(x * (xSize / 2), y * ySize, 0);

                Grid [x, y] = newPos;

				Tiles[x, y] = Instantiate (tilePrefab, newPos, Quaternion.identity) as GameObject;
				Tiles[x, y].transform.SetParent (TileParent, true);

            }
            alt = !alt;
        }
	}

	private void GenerateEnvironment(){

		int buildingChance = (int)(BuildingChance);
		int foliageChance = (int)(buildingChance + FoliageClusterChance);

        for (int x = 0; x < _max_X; x++) {
			for (int y = 0; y < _max_Y; y++) {

				int roll = Random.Range (0, 1000);
                
				if (roll <= buildingChance) {
                    GenBuildingArea(x, y);
                } else if (roll > buildingChance && roll <= foliageChance) {
                    GenFoliageCluster(x, y);
                }

            }
		}

    }

    private void GenerateDogs()
    {
        for (int x = 0; x < _max_X; x++)
        {
            for (int y = 0; y < _max_Y; y++)
            {
                int roll = Random.Range(0, 1000);
                if (roll <= DogChance)
                {
                    Objects[x, y] = Instantiate(GenDog(), Grid[x, y], Quaternion.identity) as GameObject;
                    Objects[x, y].transform.SetParent(DogParent, true);
                    Objects[x, y].GetComponent<Creature>().GameController = GameController;
                }
            }
        }
    }

    private void GenerateMonsters()
    {
        for (int x = 0; x < _max_X; x++)
        {
            for (int y = 0; y < _max_Y; y++)
            {
                int roll = Random.Range(0, 1000);
                if (roll <= MonsterChance)
                {
                    Objects[x, y] = Instantiate(GenMonster(), Grid[x, y], Quaternion.identity) as GameObject;
                    Objects[x, y].transform.SetParent(MonsterParent, true);
                    Objects[x, y].GetComponent<Creature>().GameController = GameController;
                }
            }
        }
    }

    private void GenerateOverlays()
    {
        for (int x = 0; x < _max_X; x++)
        {
            for (int y = 0; y < _max_Y; y++)
            {
                int roll = Random.Range(0, 100);
                if (roll <= OverlayChance)
                {
                    Overlays[x, y] = Instantiate(GenOverlay(), Grid[x, y], Quaternion.identity) as GameObject;
                    Overlays[x, y].transform.SetParent(EnvironmentParent, true);
                    Overlays[x, y].GetComponent<EnvironmentObject>().GameController = GameController;
                }
            }
        }
    }

    private void GenerateItems()
    {
        for (int x = 0; x < _max_X; x++)
        {
            for (int y = 0; y < _max_Y; y++)
            {
                int roll = Random.Range(0, 1000);
                if (roll <= ItemChance)
                {
                    Objects[x, y] = Instantiate(GenItem(), Grid[x, y], Quaternion.identity) as GameObject;
                    Objects[x, y].transform.SetParent(DogParent, true);
                    Objects[x, y].GetComponent<Item>().GameController = GameController;
                }
            }
        }
    }

    private GameObject GenItem()
    {
        int roll = Random.Range(0, PrefabController.Items.Count);
        return PrefabController.Items[roll];
    }

    private GameObject GenTree(){
		int roll = Random.Range (0, PrefabController.Trees.Count);
		return PrefabController.Trees [roll];
	}

	private GameObject GenPlant(){
		int roll = Random.Range (0, PrefabController.Foliage.Count);
		return PrefabController.Foliage [roll];
	}

	private GameObject GenRock(){
		int roll = Random.Range (0, PrefabController.Rocks.Count);
		return PrefabController.Rocks [roll];
	}

	private GameObject GenBuilding(){
		int roll = Random.Range (0, PrefabController.Buildings.Count);
		return PrefabController.Buildings [roll];
	}

	private GameObject GenOverlay(){
		int roll = Random.Range (0, PrefabController.Overlays.Count);
		return PrefabController.Overlays [roll];
	}

    private GameObject GenDog()
    {
        int roll = Random.Range(0, PrefabController.Dogs.Count);
        int chance = Random.Range(0, 100);
        int rarity = (int)PrefabController.Dogs[roll].GetComponent<Creature>().RarityType;
        while (chance > rarity)
        {
            roll = Random.Range(0, PrefabController.Dogs.Count);
            chance = Random.Range(0, 100);
            rarity = (int)PrefabController.Dogs[roll].GetComponent<Creature>().RarityType;
        }
        
        return PrefabController.Dogs[roll];
    }

    private GameObject GenMonster()
    {
        int roll = Random.Range(0, PrefabController.Monsters.Count);
        int chance = Random.Range(0, 100);
        int rarity = (int)PrefabController.Monsters[roll].GetComponent<Creature>().RarityType;
        while (chance > rarity)
        {
            roll = Random.Range(0, PrefabController.Dogs.Count);
            chance = Random.Range(0, 100);
            rarity = (int)PrefabController.Monsters[roll].GetComponent<Creature>().RarityType;
        }

        return PrefabController.Monsters[roll];
    }

    private void SetRandomLoc(int xCenter, int yCenter)
    {
        xPos = xCenter + Random.Range(distanceMin, distanceMax);
        if (xPos < 0)
            xPos = 0;
        if (xPos >= _max_X)
            xPos = _max_X-1;

        yPos = yCenter + Random.Range(distanceMin, distanceMax);
        if (yPos < 0)
            yPos = 0;
        if (yPos >= _max_Y)
            yPos = _max_Y - 1;
    }

	private void GenTreeArea(int xCenter, int yCenter){
		int num = Random.Range(TreeClusterMin, TreeClusterMax);

        for (int i = 0; i < num; i++) {

            SetRandomLoc(xCenter, yCenter);

            if (Objects[xPos, yPos] == null)
            {
                Objects[xPos, yPos] = Instantiate(GenTree(), Grid[xPos, yPos], Quaternion.identity) as GameObject;
                Objects[xPos, yPos].transform.SetParent(EnvironmentParent, true);
                Objects[xPos, yPos].GetComponent<EnvironmentObject>().GameController = GameController;
            }

		}
	}

	private void GenPlantArea(int xCenter, int yCenter)
    {
        int num = Random.Range(PlantClusterMin, PlantClusterMax);

        for (int i = 0; i < num; i++)
        {

            SetRandomLoc(xCenter, yCenter);

            if (Objects[xPos, yPos] == null)
            {
                Objects[xPos, yPos] = Instantiate(GenPlant(), Grid[xPos, yPos], Quaternion.identity) as GameObject;
                Objects[xPos, yPos].transform.SetParent(EnvironmentParent, true);
                Objects[xPos, yPos].GetComponent<EnvironmentObject>().GameController = GameController;
            }

        }
    }

	private void GenRockArea(int xCenter, int yCenter)
    {
        int num = Random.Range(RockClusterMin, RockClusterMax);

        for (int i = 0; i < num; i++)
        {
            SetRandomLoc(xCenter, yCenter);

            if (Objects[xPos, yPos] == null)
            {
                Objects[xPos, yPos] = Instantiate(GenRock(), Grid[xPos, yPos], Quaternion.identity) as GameObject;
                Objects[xPos, yPos].transform.SetParent(EnvironmentParent, true);
                Objects[xPos, yPos].GetComponent<EnvironmentObject>().GameController = GameController;
            }

        }

    }

	private void GenBuildingArea(int xCenter, int yCenter){

		GenPlantArea (xCenter,yCenter);

        Objects[xCenter, yCenter] = Instantiate(GenBuilding(), Grid[xCenter, yCenter], Quaternion.identity) as GameObject;
        Objects[xCenter, yCenter].transform.SetParent(EnvironmentParent, true);
        Objects[xCenter, yCenter].GetComponent<EnvironmentObject>().GameController = GameController;
        
	}

	private void GenFoliageCluster(int x, int y){
		GenRockArea (x,y);
		GenTreeArea (x,y);
		GenPlantArea (x,y);
    }

	public void RemoveFromGrid(Object obj){
		for (int x = 0; x < _max_X; x++) {
			for (int y = 0; y < _max_Y; y++) {
				if (Objects[x, y] == obj) {
					Objects[x, y] = null;
					break;
				}
			}
		}
	}

}

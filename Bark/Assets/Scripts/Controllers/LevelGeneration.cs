using UnityEngine;

public class LevelGeneration : MonoBehaviour {

    public bool SpawnMonsters = true;
    public bool SpawnDogs = true;
    public bool SpawnEnvironment = true;
    public bool SpawnOverlays = true;
    public bool SpawnItems = true;

	public GameController GameController;
	public PrefabController PrefabController;
	public Transform TileParent;
	public Transform EnvironmentParent;
	public Transform DogParent;
	public Transform MonsterParent;
	public Transform ItemParent;
	public Transform PlayerLoc;

	private Vector3[,] _grid;
	private int _max_X = 100;
	private int _max_Y = 50;

	private GameObject[,] _objects;
    public GameObject[,] Objects
    {
        get { return _objects; }
    }

    private GameObject[,] _overlays;

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
    public GameObject RightBarrier;
    public GameObject LeftBarrier;
    public GameObject SEMiddleBarrier;
    public GameObject SWMiddleBarrier;

	public int xOffset;
	public int yOffset;
	public float radiusFromPlayer;

    private float _nearClipPlane;

	void Awake(){

        _nearClipPlane = GameController.MainCamera.nearClipPlane;

        tilePrefab = GameController.PrefabController.Tiles[0];

        _grid =		new Vector3[_max_X, _max_Y];
		Tiles =		new GameObject[_max_X, _max_Y];
		_objects =	new GameObject[_max_X, _max_Y];
        _overlays =  new GameObject[_max_X, _max_Y];

		GenerateTiles();
        if(SpawnEnvironment)
		    GenerateEnvironment();
        if(SpawnMonsters)
            GenerateMonsters();
        if(SpawnDogs)
            GenerateDogs();
        if(SpawnOverlays)
            GenerateOverlays();
        if(SpawnItems)
            GenerateItems();
    }

	void Update(){

        Vector3 cameraBottomLeft = GameController.MainCamera.ViewportToWorldPoint(new Vector3(0, 0, _nearClipPlane));
        Vector3 cameraTopRight = GameController.MainCamera.ViewportToWorldPoint(new Vector3(1, 1, _nearClipPlane));

        Vector3 position;
        Vector3 size;
        for(int x = 0; x < _max_X; x++)
        {
            for(int y = 0; y < _max_Y; y++)
            {

                if(Tiles[x,y] != null) {
                    position = _grid[x, y];
                    if(position.x > cameraTopRight.x + 2 || position.x < cameraBottomLeft.x - 2 || position.y > cameraTopRight.y + 1 || position.y < cameraBottomLeft.y - 1) {
                        Tiles[x, y].SetActive(false);
                    } else if (position.x - cameraTopRight.x < 2 || position.x - cameraBottomLeft.x < -2 || position.y - cameraTopRight.y < 1 || position.y - cameraBottomLeft.y < -1) {
                        Tiles[x, y].SetActive(true);
                    }
                }

                if (_objects[x, y] != null) {
                    position = _objects[x, y].transform.position;
                    size = _objects[x, y].transform.lossyScale;
                    if (position.x - size.x/2.0f > cameraTopRight.x + 5 || position.x + size.x / 2.0f < cameraBottomLeft.x - 5 || position.y - size.y / 2.0f > cameraTopRight.y + 5 || position.y + size.y / 2.0f < cameraBottomLeft.y - 5) {
                        _objects[x, y].SetActive(false);
                    } else if (position.x - cameraTopRight.x < 5 || position.x - cameraBottomLeft.x < -5 || position.y - cameraTopRight.y < 5 || position.y - cameraBottomLeft.y < -5) {
                        _objects[x, y].SetActive(true);
                    }
                }

                if (_overlays[x, y] != null) {
                    position = _overlays[x, y].transform.position;
                    size = _overlays[x, y].transform.lossyScale;
                    if (position.x - size.x / 2.0f > cameraTopRight.x + 5 || position.x + size.x / 2.0f < cameraBottomLeft.x - 5 || position.y - size.y > cameraTopRight.y + 5 || position.y + size.y < cameraBottomLeft.y - 5) {
                        _overlays[x, y].SetActive(false);
                    } else if (position.x - cameraTopRight.x < 5 || position.x - cameraBottomLeft.x < -5 || position.y - cameraTopRight.y < 5 || position.y - cameraBottomLeft.y < -5) {
                        _overlays[x, y].SetActive(true);
                    }
                }
            }
        }

    }

    private void GenerateTiles(){
        
		
        float tileSize = 2.525f;

        Vector2 xyVector;
        Vector3 newPos;

        Vector2 dx = new Vector2(Mathf.Sqrt(2) / 2.0f, Mathf.Sqrt(2) / 4.0f);
        Vector2 dy = new Vector2(Mathf.Sqrt(2) / 2.0f, -Mathf.Sqrt(2) / 4.0f);

        for (int x = 0; x < _max_X; x++) {
            for (int y = 0; y < _max_Y; y++) {

                xyVector = tileSize * (x * dx + y * dy);
                newPos = new Vector3(xyVector.x, xyVector.y, 0.0f);

                GameObject objToCreate = tilePrefab;

                // decide whether a barrier or tile should be created
                // barriers also need slight manual adjustments on position
                if(x == 0) {
                    if (y == 0 || y == _max_Y - 1) {
                        if(y == 0) {
                            newPos.x += 1.06f;
                            newPos.y += -0.44f;
                        } else {
                            newPos.x += 0.21f;
                            newPos.y += 0.8f;
                        }
                        objToCreate = LeftBarrier;
                    } else {
                        objToCreate = SWMiddleBarrier;
                    } 
                } else if (x == _max_X - 1) {
                    if (y == 0 || y == _max_Y - 1) {
                        objToCreate = RightBarrier;
                    } else {
                        newPos.x -= 1f;
                        newPos.y -= 1f;
                        objToCreate = SWMiddleBarrier;
                    }
                } else if (y == 0 || y == _max_Y - 1) {
                    if(y == 0) {
                        newPos.x += 1.06f;
                        newPos.y += -0.29f;
                    } else {
                        newPos.x += 0.21f;
                        newPos.y += 0.8f;
                    }
                    objToCreate = SEMiddleBarrier;
                }

                _grid[x, y] = newPos;
                Tiles[x, y] = Instantiate(objToCreate, newPos, Quaternion.identity) as GameObject;
                Tiles[x, y].transform.SetParent(TileParent, true);
                if (Tiles[x, y].GetComponent<EnvironmentObject>() != null)
                    Tiles[x, y].GetComponent<EnvironmentObject>().GameController = GameController;

            }
        }

    }

    private void GenerateEnvironment(){

		int buildingChance = (int)(BuildingChance);
		int foliageChance = (int)(buildingChance + FoliageClusterChance);

        for (int x = 1; x < _max_X - 2; x++) {
			for (int y = 1; y < _max_Y - 2; y++) {
                
                    int roll = Random.Range(0, 1000);

                    if (roll <= buildingChance) {
                        GenBuildingArea(x, y);
                    }
                    else if (roll > buildingChance && roll <= foliageChance) {
                        GenFoliageCluster(x, y);
                    }
                    
            }
		}

    }

    private void GenerateDogs()
    {
        for (int x = 1; x < _max_X - 2; x++) {
            for (int y = 0; y < _max_Y - 2; y++) {
                int roll = Random.Range(0, 1000);
                if (roll <= DogChance) {
                    _objects[x, y] = Instantiate(GenDog(), _grid[x, y], Quaternion.identity) as GameObject;
                    _objects[x, y].transform.SetParent(DogParent, true);
                    _objects[x, y].GetComponent<Creature>().GameController = GameController;
                }  
            }
        }
    }

    private void GenerateMonsters()
    {
        for (int x = 1; x < _max_X - 2; x++) {
            for (int y = 1; y < _max_Y - 2; y++) {
                int roll = Random.Range(0, 1000);
                if (roll <= MonsterChance) {
                    _objects[x, y] = Instantiate(GenMonster(), _grid[x, y], Quaternion.identity) as GameObject;
                    _objects[x, y].transform.SetParent(MonsterParent, true);
                    _objects[x, y].GetComponent<Creature>().GameController = GameController;
                }
            }
        }
    }

    private void GenerateOverlays()
    {
        for (int x = 1; x < _max_X - 2; x++) {
            for (int y = 1; y < _max_Y - 2; y++) {
                int roll = Random.Range(0, 100);
                if (roll <= OverlayChance) {
                    _overlays[x, y] = Instantiate(GenOverlay(), _grid[x, y], Quaternion.identity) as GameObject;
                    _overlays[x, y].transform.SetParent(EnvironmentParent, true);
                    _overlays[x, y].GetComponent<EnvironmentObject>().GameController = GameController;
                }
            }
        }
    }

    private void GenerateItems()
    {
        for (int x = 1; x < _max_X - 2; x++) {
            for (int y = 1; y < _max_Y - 2; y++) { 
                int roll = Random.Range(0, 1000);
                if (roll <= ItemChance) {
                    _objects[x, y] = Instantiate(GenItem(), _grid[x, y], Quaternion.identity) as GameObject;
                    _objects[x, y].transform.SetParent(ItemParent, true);
                    _objects[x, y].GetComponent<Item>().GameController = GameController;
                } 
            }
        }
    }

    public GameObject GenItem()
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
        // also prevent that random loc from being too close to barriers
        xPos = xCenter + Random.Range(distanceMin, distanceMax);
        if (xPos < 2)
            xPos = 2;
        if (xPos >= _max_X - 2)
            xPos = _max_X - 3;

        yPos = yCenter + Random.Range(distanceMin, distanceMax);
        if (yPos < 2)
            yPos = 2;
        if (yPos >= _max_Y - 2)
            yPos = _max_Y - 2;
    }

	private void GenTreeArea(int xCenter, int yCenter){
		int num = Random.Range(TreeClusterMin, TreeClusterMax);

        for (int i = 0; i < num; i++) {

            SetRandomLoc(xCenter, yCenter);

            if (_objects[xPos, yPos] == null) {
                _objects[xPos, yPos] = Instantiate(GenTree(), _grid[xPos, yPos], Quaternion.identity) as GameObject;
                _objects[xPos, yPos].transform.SetParent(EnvironmentParent, true);
                _objects[xPos, yPos].GetComponent<EnvironmentObject>().GameController = GameController;
            }

		}
	}

	private void GenPlantArea(int xCenter, int yCenter)
    {
        int num = Random.Range(PlantClusterMin, PlantClusterMax);

        for (int i = 0; i < num; i++) {

            SetRandomLoc(xCenter, yCenter);

            if (_objects[xPos, yPos] == null) {
                _objects[xPos, yPos] = Instantiate(GenPlant(), _grid[xPos, yPos], Quaternion.identity) as GameObject;
                _objects[xPos, yPos].transform.SetParent(EnvironmentParent, true);
                _objects[xPos, yPos].GetComponent<EnvironmentObject>().GameController = GameController;
            }

        }
    }

	private void GenRockArea(int xCenter, int yCenter)
    {
        int num = Random.Range(RockClusterMin, RockClusterMax);

        for (int i = 0; i < num; i++) {
            SetRandomLoc(xCenter, yCenter);

            if (_objects[xPos, yPos] == null) {
                _objects[xPos, yPos] = Instantiate(GenRock(), _grid[xPos, yPos], Quaternion.identity) as GameObject;
                _objects[xPos, yPos].transform.SetParent(EnvironmentParent, true);
                _objects[xPos, yPos].GetComponent<EnvironmentObject>().GameController = GameController;
            }
        }
    }

	private void GenBuildingArea(int xCenter, int yCenter){

        GenPlantArea (xCenter,yCenter);

        _objects[xCenter, yCenter] = Instantiate(GenBuilding(), _grid[xCenter, yCenter], Quaternion.identity) as GameObject;
        _objects[xCenter, yCenter].transform.SetParent(EnvironmentParent, true);
        _objects[xCenter, yCenter].GetComponent<EnvironmentObject>().GameController = GameController;
        
	}

	private void GenFoliageCluster(int x, int y){
		GenRockArea (x,y);
		GenTreeArea (x,y);
		GenPlantArea (x,y);
    }

	public void RemoveFromGrid(GameObject obj){
		for (int x = 0; x < _max_X; x++) {
			for (int y = 0; y < _max_Y; y++) {
				if (_objects[x, y] == obj) {
					_objects[x, y] = null;
					break;
				}
			}
		}
	}

}

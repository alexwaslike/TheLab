using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private List<DogStats> _dogStatItems;
    public List<DogStats> DogStatItems
    {
        get { return _dogStatItems; }
    }

    public GameController GameController;
    public GameObject DogStatsPrefab;

	private float DogStatsBuffer = Screen.height/4.0f;
    private float _dogStatsYStart = 108.0f;

    void Start()
    {
        _dogStatItems = new List<DogStats>();
    }

    public void AddNewDogStats(Dog dog)
    {
        GameObject newStats = Instantiate(DogStatsPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        _dogStatItems.Add(newStats.GetComponent<DogStats>());
        newStats.transform.SetParent(transform, false);
        newStats.transform.localPosition = new Vector3(DogStatsPrefab.transform.position.x, _dogStatsYStart + DogStatsBuffer * -(_dogStatItems.Count-1), 0);

        newStats.GetComponentInChildren<HealthBar>().Health = dog.Health;
        newStats.GetComponent<DogStats>().Dog = dog;
		newStats.GetComponent<DogStats>().HUD = this;
    }

    public void RemoveDogStats(Dog dog)
    {
        int dogStat = GetIndexOfDogStat(dog);
        DogStats dogToRemove = _dogStatItems[dogStat];
        _dogStatItems.Remove(dogToRemove);
        Destroy(dogToRemove.gameObject);

        for (int j = 0; j<_dogStatItems.Count; j++) {
            _dogStatItems[j].transform.localPosition = new Vector3(DogStatsPrefab.transform.position.x, _dogStatsYStart + DogStatsBuffer * -j, 0);
        }
    }

	public void ViewDogInventoryClicked(Dog dog){
		GameController.DogInventory.SetActive (true);
		GameController.DogInventory.GetComponent<Inventory> ().IconClicked (dog.GetComponent<Collectible>());
	}

	public void ViewItemInventoryClicked(){
		GameController.ItemInventory.SetActive(true);
	}

    public int GetIndexOfDogStat(Dog dog)
    {
        int index = -1;
        int i = 0;
        bool foundDog = false;
        while (!foundDog && i < _dogStatItems.Count)
        {
            DogStats dogStat = _dogStatItems[i];
            if (dogStat.Dog == dog)
            {
                index = i;
                foundDog = true;
            }
            i++;
        }
        return index;
    }

}
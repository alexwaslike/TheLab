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

	private float DogStatsBuffer = Screen.height/7.0f;

    void Start()
    {
        _dogStatItems = new List<DogStats>();
    }

    public void AddNewDogStats(Dog dog)
    {
        GameObject newStats = Instantiate(DogStatsPrefab, new Vector3(DogStatsPrefab.transform.position.x, DogStatsPrefab.transform.position.y, 0.0f), Quaternion.identity) as GameObject;

        _dogStatItems.Add(newStats.GetComponent<DogStats>());
        newStats.transform.SetParent(transform, false);
        newStats.transform.Translate(new Vector3(0, DogStatsBuffer * -(_dogStatItems.Count-1), 0));

        newStats.GetComponentInChildren<HealthBar>().Health = dog.Health;
        newStats.GetComponent<DogStats>().Dog = dog;
		newStats.GetComponent<DogStats>().HUD = this;
    }

    public void RemoveDogStats(Dog dog)
    { 
        foreach (DogStats dogStat in _dogStatItems) {
            if (dogStat.Dog == dog) {
                _dogStatItems.Remove(dogStat);
                Destroy(dogStat.gameObject);
                break;
            }
        }

        for(int i=0; i<_dogStatItems.Count; i++) {
            _dogStatItems[i].transform.Translate(new Vector3(0, DogStatsBuffer, 0));
        }
    }

	public void ViewDogInventoryClicked(Dog dog){
		GameController.DogInventory.SetActive (true);
		GameController.DogInventory.GetComponent<DogCollectionUI> ().DisplayStats (dog);
	}
	

	public void CloseDogInventoryClicked(){
		GameController.DogInventory.SetActive(false);
	}

}
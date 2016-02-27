using UnityEngine;
using UnityEngine.UI;

public class DogStats : MonoBehaviour {

	public HUD HUD;

    public Dog Dog;
	public Image DogImage;
    public Text NameText;
    
	void Start () {
        NameText.text = Dog.GetComponent<Creature>().Name;
		DogImage.sprite = Dog.GetComponent<Creature>().Sprite_S;
		GetComponentInChildren<Button>().onClick.AddListener(delegate { this.StatsClicked(); } );
	}

	void StatsClicked(){
		HUD.ViewDogInventoryClicked(Dog);
	}
}

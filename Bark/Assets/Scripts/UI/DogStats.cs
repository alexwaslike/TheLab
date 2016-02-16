using UnityEngine;
using UnityEngine.UI;

public class DogStats : MonoBehaviour {

    public Dog Dog;
    public Text NameText;
    
	void Start () {
        NameText.text = Dog.GetComponent<Creature>().Name;
	}
}

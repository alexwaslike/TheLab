using UnityEngine;
using System.Collections;

public class DogTrait : MonoBehaviour {

	public string Name;
	public string Description;
	public Sprite icon;

	public Dog Dog;

	protected MainCharacter _player;

	public virtual void OnAttach(MainCharacter mainCharacter){Debug.Log ("not implemented");}

	public virtual void OnDetach(){Debug.Log ("not implemented");}


}

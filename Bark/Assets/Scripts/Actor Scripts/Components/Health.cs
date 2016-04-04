using UnityEngine;

public class Health : MonoBehaviour {

	private int _health;
	public int HP
	{
		get { return _health; }
	}

	public int MaxHealth;

	void Start(){
		_health = MaxHealth;
	}

	public void TakeDamage(int damage)
	{
		if (_health - damage <= 0) {
			_health = 0;

			if (GetComponent<Dog> () != null)
				GetComponent<Dog> ().Death ();
			else if (GetComponent<Monster> () != null)
				GetComponent<Monster> ().Death ();
			else if (GetComponent<MainCharacter> () != null)
				GetComponent<MainCharacter> ().Death ();
			else
				Debug.Log ("No death implemented for this actor!");
		}
		else _health -= damage;
	}
}

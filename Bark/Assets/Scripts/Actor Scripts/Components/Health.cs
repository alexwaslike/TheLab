using UnityEngine;

public class Health : MonoBehaviour {

	private float _health;
	public float HP
	{
		get { return _health; }
	}

	public int MaxHealth;

	void Start(){
		_health = MaxHealth;
	}

	public void TakeDamage(float damage)
	{
        if (_health - damage <= 0)
        {
            _health = 0;

            if (GetComponent<Dog>() != null)
                GetComponent<Dog>().Death();
            else if (GetComponent<Monster>() != null)
                GetComponent<Monster>().Death();
            else if (GetComponent<MainCharacter>() != null)
                GetComponent<MainCharacter>().Death();
            else
                Debug.Log("No death implemented for this actor!");
        }
        else {

            if(GetComponent<CombatAI>() != null)
            {
                float reduction = damage * GetComponent<CombatAI>().DamageReduction;
                damage -= reduction;

                int roll = Random.Range(0, 100);
                if (roll > 0 && roll < GetComponent<CombatAI>().DodgeChance)
                    damage = 0;
            }

            _health -= damage;

        }
	}
}

using UnityEngine;

public class Health : MonoBehaviour {

	private float _health;
	public float HP
	{
		get { return _health; }
	}

    public GameObject TakingDamageUI;

    [System.NonSerialized]
    public int MaxHealth;

	void Start(){
		_health = MaxHealth;
	}

    public void Heal(float healAmount)
    {
        if (_health + healAmount > MaxHealth)
            _health = MaxHealth;
        else
            _health += healAmount;
    }

	public void TakeDamage(float damage)
	{
        GameObject damageObj = Instantiate(TakingDamageUI, Vector3.zero, Quaternion.identity) as GameObject;
        damageObj.transform.parent = gameObject.transform;
        damageObj.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        damageObj.transform.localPosition = new Vector3(Random.Range(-transform.localScale.x, transform.localScale.x), Random.Range(-transform.localScale.y, transform.localScale.y), 2);

        if(GetComponent<Creature>() != null)
        {
            Creature creature = GetComponent<Creature>();
            if (creature.AudioSource != null && creature.HurtSound != null)
            {
                creature.AudioSource.pitch = Random.Range(creature.MinPitch, creature.MaxPitch);
                creature.AudioSource.PlayOneShot(creature.HurtSound);
            }
        }

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

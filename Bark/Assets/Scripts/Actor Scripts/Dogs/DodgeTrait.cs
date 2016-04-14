using UnityEngine;

// Dodge: 25% chance of monster missing
public class DodgeTrait : DogTrait {

	void Start()
    {
        GetComponent<CombatAI>().DodgeChance = 25f;
    }

}

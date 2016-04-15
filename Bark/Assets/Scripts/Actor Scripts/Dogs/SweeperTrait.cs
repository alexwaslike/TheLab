using UnityEngine;

// Sweeper- Increase monster drop rate by 50%
public class SweeperTrait : DogTrait {

	void Start()
    {
        GetComponent<CombatAI>().DropRateIncrease = 2;
    }

}

using UnityEngine;
using System.Collections;

// Tankie - Reduce monster attack power by 33%
public class TankTrait : DogTrait {

    void Start()
    {
        GetComponent<CombatAI>().DamageReduction = .25f;
    }

}

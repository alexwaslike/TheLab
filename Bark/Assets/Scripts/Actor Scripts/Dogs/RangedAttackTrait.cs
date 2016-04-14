using UnityEngine;

// interaction range very long
public class RangedAttackTrait : MonoBehaviour {

	void Start () {
        GetComponent<CombatAI>().InteractionRange *= 2.0f;
	}

}


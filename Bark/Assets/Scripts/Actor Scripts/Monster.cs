using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(CombatAI))]

public class Monster : MonoBehaviour {

    public Creature Creature;
    public CombatAI CombatAI;

    private Dog _currentTarget;

	void Start ()
    {
        Creature = GetComponent<Creature>();
        CombatAI = GetComponent<CombatAI>();

        Creature.GameController.SetSortingOrder (gameObject);
        Creature.ChangeState(State.Idle);
        CombatAI.InteractionRange = 6.0f;
    }

	void Update ()
    {

        if (Creature.CurrentState == State.Attack) {
            CombatAI.TryAttackDog();
        } else if (Creature.CurrentState == State.Idle) {

            // walk around...

            if (CombatAI.WithinInteractionRange(Creature.GameController.MainCharacterObj))
                Creature.ChangeState(State.Attack);
		}

        Creature.GameController.SetSortingOrder(gameObject);
    }

	public void Death(){
		Creature.ChangeState (State.Dead);
		Creature.GameController.CombatController.RemoveFromCombat (CombatAI);
		Destroy (gameObject);
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(CombatAI))]

public class Monster : MonoBehaviour {

    public Creature Creature;
    public CombatAI CombatAI;

	void Start ()
    {
        Creature.ChangeState(State.Idle);
    }

	void Update ()
    {
        if (Creature.CurrentState == State.Attack) {

            if (CombatAI.HasTarget)
            {
                float xShift = (CombatAI.CurrentTarget.transform.position.x - transform.position.x) * Creature.Speed;
                float yShift = (CombatAI.CurrentTarget.transform.position.y - transform.position.y) * Creature.Speed;
                Creature.Move(xShift, yShift);
            }
            CombatAI.TryAttackDog();

        } else if (Creature.CurrentState == State.Idle) {

            // walk around...

            if (CombatAI.WithinInteractionRange(Creature.GameController.MainCharacterObj))
                Creature.ChangeState(State.Attack);
		}
    }

	public void Death(){
		Creature.ChangeState (State.Dead);
		Creature.GameController.CombatController.RemoveFromCombat (CombatAI);
		Destroy (gameObject);
	}
}

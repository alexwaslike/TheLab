using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]

public class CombatAI : MonoBehaviour {

    private int _attackRate = 10;
    public int AttackRate
    {
        get { return _attackRate; }
    }

	private Health _currentTarget;
	public Health CurrentTarget
    {
        get { return _currentTarget; }
    }

    private int _attackCooldown;

    public GameController GameController;
    public Creature Creature;
	public Health Health;
    public float InteractionRange = 2.0f;
	public int AttackDamage = 10;
	public bool HasTarget;

    void Start()
    {
        Creature = GetComponent<Creature>();
        if (Creature != null)
            GameController = Creature.GameController;

		Health = GetComponent<Health> ();

        _attackCooldown = 0;
        HasTarget = false;
    }

    public bool WithinInteractionRange(GameObject otherObject)
    {
        if (Vector3.Distance(transform.position, otherObject.transform.position) <= InteractionRange)
            return true;
        return false;
    }

    public void TryAttackDog()
    {
        if (Creature != null)
        {
            if (!WithinInteractionRange(GameController.MainCharacterObj)){
                Creature.ChangeState(State.Idle);
				GameController.CombatController.RemoveFromCombat (this);
            }
			else if (Creature.GameController.CombatController.CanAttackDog()){

				if(!GameController.CombatController.EngagedAI.Contains(this))
					GameController.CombatController.AddToCombat (this);

				if (HasTarget)
                {
                    Attack();
                }
                else {

					if (Creature.GameController.MainCharacter.DogInventory.Count > 0)
						_currentTarget = GameController.MainCharacter.DogInventory [0].Health;
					else
						_currentTarget = GameController.MainCharacter.Health;
                    HasTarget = true;
                    Attack();

                }
            }
            else {
                HasTarget = false;
                // TODO: strafe around for a bit
            }
        }
        else Debug.Log("Attempting to use Creature Attack method with non-Creature");
    }

	public void TryAttackMonster(){
		
		if(!GameController.CombatController.EngagedAI.Contains(this))
			GameController.CombatController.AddToCombat (this);

		if (HasTarget) {
			Attack ();
		} else {

            bool foundTarget = false;
            foreach (CombatAI AI in GameController.CombatController.EngagedAI) {
				if (AI.GetComponent<Monster> () != null) {
					_currentTarget = AI.GetComponent<Health> ();
                    foundTarget = true;
					break;
				}
			}
            if (foundTarget) {
                HasTarget = true;
                Attack();
            } else {
                Creature.ChangeState(State.Follow);
            }

		}
        
	}

    private void Attack()
    {
		if (_currentTarget.GetComponent<CombatAI> () != null)
			_currentTarget.GetComponent<CombatAI> ().BeingAttacked (this);

        if (_attackCooldown == 0)
        {
            _attackCooldown = _attackRate;
			_currentTarget.TakeDamage(AttackDamage);
        }
        else {
            _attackCooldown--;
        }
    }

	private void BeingAttacked(CombatAI attacker){
		if (_currentTarget != attacker.Health) {
			Creature.ChangeState (State.Attack);
			GameController.CombatController.AddToCombat (this);
			_currentTarget = attacker.GetComponent<Health>();
			HasTarget = true;
		}
	}


}

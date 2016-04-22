using UnityEngine;
using System.Collections.Generic;

public class CombatController : MonoBehaviour {

	private List<CombatAI> _engagedAI;
	public List<CombatAI> EngagedAI{
		get{ return _engagedAI; }
	}

	public GameController GameController;

	public float MaxAttackersMultiplier = 1.5f;
	public int CurrentNumAttackers = 0;

    public enum RarityType
    {
        Rare = 10,
        MediumRare = 20,
        Common = 70
    }

    public enum HealthType
    {
        High = 80,
        MedHigh = 50,
        Med = 30,
        LowMed = 20,
        Low = 10
    }

    public enum DamageType
    {
        High = 15,
        MedHigh = 10,
        Med = 7,
        LowMed = 3,
        Low = 1
    }

    public enum AttackRateType
    {
        High = 1,
        MedHigh = 2,
        Med = 3,
        LowMed = 4,
        Low = 5
    }

    public enum MovementSpeedType
    {
        High = 10,
        MedHigh = 8,
        Med = 7,
        LowMed = 6,
        Low = 4
    }

    void Start()
    {
        _engagedAI = new List<CombatAI>();
    }

    public void AddToCombat(CombatAI combatAI){
		_engagedAI.Add (combatAI);

        if (combatAI.GetComponent<Monster>() != null) {
            GameController.LevelGeneration.RemoveFromGrid(gameObject);
            foreach (Dog dog in GameController.MainCharacter.DogInventory) {
                if (dog.Creature.CurrentState == State.Follow && !dog.CombatAI.HasTarget) {
                    dog.CombatAI.BeingAttacked(combatAI);
                }
            }
        } else if (combatAI.GetComponent<Dog>() != null) {
            foreach (CombatAI ai in EngagedAI) {
                if(ai != null && ai.GetComponent<Monster>() != null)
                    ai.GetNewDogTarget();
            }
        }
    }

	public void RemoveFromCombat(CombatAI aiToRemove){

		aiToRemove.HasTarget = false;
		_engagedAI.Remove (aiToRemove);

        int numEngagedMonsters = 0;
		foreach(CombatAI AI in _engagedAI){

			if (AI != null && (AI.CurrentTarget == null || AI.CurrentTarget == aiToRemove.Health)) {
				AI.HasTarget = false;
			}
            if(AI != null && AI.GetComponent<Monster>() != null) {
                AI.GetNewDogTarget();
                numEngagedMonsters++;
            }

		}

        if (numEngagedMonsters == 0)
            CombatOver();

	}

	public bool CanAttackDog()
	{
		if(GameController.MainCharacter.DogInventory.Count > 0 && CurrentNumAttackers + 1 <= MaxAttackersMultiplier * GameController.MainCharacter.DogInventory.Count) {
			return true;
		}
		else if(CurrentNumAttackers + 1 <= MaxAttackersMultiplier) {
			return true;
		}
		return false;

	}

    private void CombatOver()
    {
        foreach(CombatAI AI in _engagedAI) {
            if(AI != null) {
                AI.HasTarget = false;
                AI.GetComponent<Creature>().ChangeState(State.Follow);
            }
        }
        _engagedAI.Clear();
    }

}

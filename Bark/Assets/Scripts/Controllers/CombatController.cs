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
        High = 150,
        MedHigh = 125,
        Med = 100,
        LowMed = 75,
        Low = 50
    }

    public enum DamageType
    {
        High = 5,
        MedHigh = 4,
        Med = 3,
        LowMed = 2,
        Low = 1
    }

    public enum AttackRateType
    {
        High = 8,
        MedHigh = 5,
        Med = 4,
        LowMed = 2,
        Low = 1
    }

    public enum MovementSpeedType
    {
        High = 10,
        MedHigh = 8,
        Med = 7,
        LowMed = 6,
        Low = 4
    }

    public void AddToCombat(CombatAI combatAI){
		_engagedAI.Add (combatAI);
	}

	public void RemoveFromCombat(CombatAI aiToRemove){

		aiToRemove.HasTarget = false;
		_engagedAI.Remove (aiToRemove);

		foreach(CombatAI AI in _engagedAI){

			if (AI != null && (AI.CurrentTarget == null || AI.CurrentTarget == aiToRemove.Health)) {
				AI.HasTarget = false;
			}
		}

	}

	void Start(){
		_engagedAI = new List<CombatAI> ();
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

}

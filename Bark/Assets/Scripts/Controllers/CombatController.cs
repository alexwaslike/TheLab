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
        High = 30,
        MedHigh = 20,
        Med = 15,
        LowMed = 10,
        Low = 5
    }

    public enum AttackRateType
    {
        High = 10,
        MedHigh = 20,
        Med = 40,
        LowMed = 80,
        Low = 120
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

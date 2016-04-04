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

    public enum TraitType
    {
        High, MedHigh, Med, LowMed, Low
    }

    public int HighHealth = 150;
    public int MedHighHealth = 125;
    public int MedHealth = 100;
    public int LowMedHealth = 75;
    public int LowHealth = 50;

    public int HighDamage = 5;
    public int MedHighDamage = 4;
    public int MedDamange = 3;
    public int LowMedDamange = 2;
    public int LowDamange = 1;

    public int HighAtkRate = 5;
    public int MedHighAtkRate = 4;
    public int MedAtkRate = 8;
    public int LowMedAtkRate = 2;
    public int LowAtkRate = 1;

    public float HighMoveSpeed = 5.0f;
    public float MedHighMoveSpeed = 4.0f;
    public float MedMoveSpeed = 3.0f;
    public float LowMedMoveSpeed = 2.0f;
    public float LowMoveSpeed = 1.0f;


    public int TraitDamageAmount(TraitType damageType)
    {
        switch (damageType)
        {
            case TraitType.High: return HighDamage;
            case TraitType.MedHigh: return MedHighDamage;
            case TraitType.Med: return MedDamange;
            case TraitType.LowMed: return LowMedDamange;
            case TraitType.Low: return LowDamange;
            default: Debug.LogError("Damage TraitType not implemented!"); return 0;
        }
    }

    public int TraitHealthAmount(TraitType healthType)
    {
        switch (healthType)
        {
            case TraitType.High: return HighHealth;
            case TraitType.MedHigh: return MedHighHealth;
            case TraitType.Med: return MedHealth;
            case TraitType.LowMed: return LowMedHealth;
            case TraitType.Low: return LowHealth;
            default: Debug.LogError("Health TraitType not implemented!"); return 0;
        }
    }

    public int TraitAttackSpeed(TraitType atkSpeedType)
    {
        switch (atkSpeedType)
        {
            case TraitType.High: return HighAtkRate;
            case TraitType.MedHigh: return MedHighAtkRate;
            case TraitType.Med: return MedAtkRate;
            case TraitType.LowMed: return LowMedAtkRate;
            case TraitType.Low: return LowAtkRate;
            default: Debug.LogError("Attack Speed TraitType not implemented!"); return 0;
        }
    }

    public float TraitMoveSpeed(TraitType moveSpeedType)
    {
        switch (moveSpeedType)
        {
            case TraitType.High: return HighMoveSpeed;
            case TraitType.MedHigh: return MedHighMoveSpeed;
            case TraitType.Med: return MedMoveSpeed;
            case TraitType.LowMed: return LowMedMoveSpeed;
            case TraitType.Low: return LowMoveSpeed;
            default: Debug.LogError("Move Speed TraitType not implemented!"); return 0;
        }
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

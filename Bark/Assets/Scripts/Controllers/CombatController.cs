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

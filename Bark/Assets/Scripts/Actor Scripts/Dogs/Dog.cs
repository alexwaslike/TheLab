using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(CombatAI))]

public class Dog : MonoBehaviour {

	protected string _species = "doge";
    public string Species
    {
        get { return _species; }
    }

	public List<DogTrait> Traits;

	protected float _dogDistance = 1.0f;

	public GameObject Shadow;
    public Creature Creature;
    public CombatAI CombatAI;
	public Health Health;

	void Start ()
    {
        Creature.ChangeState (State.Box);
	}

	void Update ()
    {
		if (Creature.CurrentState == State.Follow) {

            PositionDog(Creature.GameController.MainCharacter.DogInventory.IndexOf(this));

		} else if (Creature.CurrentState == State.Attack) {

			CombatAI.TryAttackMonster ();
            PositionDog(Creature.GameController.MainCharacter.DogInventory.IndexOf(this));

		} else if (Creature.CurrentState == State.Box ) {

            if (CombatAI.WithinInteractionRange(Creature.GameController.MainCharacterObj))
                Shadow.SetActive(true);
            else
                Shadow.SetActive(false);

        }
	}

	public void OnMouseUp()
    {
        if (CombatAI.WithinInteractionRange(Creature.GameController.MainCharacterObj))
            Clicked();
	}

	protected void Clicked()
    {
		if (Creature.CurrentState == State.Box) {
            Creature.GameController.DogClicked (this);
		}
	}

	public void Attached(MainCharacter mainCharacter){
		foreach(DogTrait trait in Traits){
			trait.OnAttach (mainCharacter);
		}
	}

	public void Detached(){
		foreach(DogTrait trait in Traits){
			trait.OnDetach ();
		}
	}

    public void PositionDog(int index)
    {

        MainCharacter character = Creature.GameController.MainCharacter;

		if (character.DogInventory.Count > 0) {
			float radians = ((360 / character.DogInventory.Count) * index) * (Mathf.PI / 180.0f);
			float xLoc = character.transform.position.x + (Mathf.Cos (radians) * _dogDistance);
			float yLoc = character.transform.position.y + (Mathf.Sin (radians) * _dogDistance);

			Creature.Move ((xLoc - transform.position.x) * Creature.Speed, (yLoc - transform.position.y) * Creature.Speed);
		} else
			Debug.LogWarning ("Dog not in inventory!!");

    }

	public void Death(){
		Creature.ChangeState (State.Dead);
		CombatAI.GameController.DogDeath (this);
	}

}

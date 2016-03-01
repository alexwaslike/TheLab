using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(CombatAI))]
[RequireComponent(typeof(Collectible))]
[RequireComponent(typeof(BoxCollider2D))]

public class Dog : MonoBehaviour {

	public List<DogTrait> Traits;

	private float _dogDistance = 1.0f;

	public GameObject Shadow;
    public Creature Creature;
    public CombatAI CombatAI;
	public Health Health;
	public Collectible Collectible;

	void Start ()
    {
		string description = "Traits:\n";
		foreach (DogTrait trait in Traits) {
			description += trait.Name + ": " + trait.Description + "\n";
		}
		Collectible.Description = description;
		Collectible.Name = Creature.Name;
		Collectible.Sprite = Creature.Sprite_S;

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

	private void Clicked()
    {
		if (Creature.CurrentState == State.Box) {
            Creature.GameController.DogClicked (this);
		}
	}

	public void Attached(MainCharacter mainCharacter){
		Shadow.SetActive(false);
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

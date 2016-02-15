﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(CombatAI))]

public class Dog : MonoBehaviour {

	private string _species = "doge";
    public string Species
    {
        get { return _species; }
    }

    private float DogDistance = 1.0f;

    public GameObject Shadow;
    public Creature Creature;
    public CombatAI CombatAI;

	void Start ()
    {
        Creature = GetComponent<Creature>();
        CombatAI = GetComponent<CombatAI>();

		Creature.SpriteRenderer = GetComponent<SpriteRenderer> ();

        Creature.GameController.SetSortingOrder (gameObject);
        Creature.ChangeState (State.Box);
	}

	void Update ()
    {
		if (Creature.CurrentState == State.Follow) {

            PositionDog(Creature.GameController.MainCharacter.DogInventory.IndexOf(this));
            Creature.GameController.SetSortingOrder (gameObject);

		} else if (Creature.CurrentState == State.Attack) {

            Creature.GameController.SetSortingOrder (gameObject);

		} else if (Creature.CurrentState == State.Box ){

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

    public void PositionDog(int index)
    {
        MainCharacter character = Creature.GameController.MainCharacter;
        float radians = ((360 / character.DogInventory.Count) * index) * (Mathf.PI / 180.0f);
        float xLoc = character.transform.position.x + (Mathf.Cos(radians) * DogDistance);
        float yLoc = character.transform.position.y + (Mathf.Sin(radians) * DogDistance);

        Creature.Move((xLoc - transform.position.x) * Creature.Speed, (yLoc - transform.position.y) * Creature.Speed);

    }

}

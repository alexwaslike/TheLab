﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(CombatAI))]
[RequireComponent(typeof(Collectible))]
[RequireComponent(typeof(BoxCollider2D))]

public class Dog : MonoBehaviour {

	public List<DogTrait> Traits;

	public float DogDistance = 1.0f;

	public GameObject Shadow;
    public Creature Creature;
    public CombatAI CombatAI;
	public Health Health;
	public Collectible Collectible;

    void Start ()
    {

        Collectible.Description = WritingDB.DogDescriptions[Creature.Name];
		Collectible.Name = Creature.Name;
		Collectible.Sprite = Creature.Sprite_S;
		Collectible.Inventory = Creature.GameController.DogInventory.GetComponent<Inventory>();

        Creature.ChangeState (State.Box);
	}

	void Update ()
    {
		if (Creature.CurrentState == State.Follow) {
			
			PositionDog ();

		} else if (Creature.CurrentState == State.Attack) {

			CombatAI.TryAttackMonster ();
			PositionDog ();

		} else if (Creature.CurrentState == State.Box) {
            
			if (CombatAI.WithinRange (Creature.GameController.MainCharacterObj, CombatAI.InteractionRange))
				Shadow.SetActive (true);
			else
				Shadow.SetActive (false);

		}
	}

	public void OnMouseUp()
    {
		if (CombatAI.WithinRange(Creature.GameController.MainCharacterObj, CombatAI.InteractionRange))
            Clicked();
	}

	private void Clicked()
    {
        if (Creature.CurrentState == State.Box) {
            Creature.GameController.DogClicked (this);
		}
	}

	public void Attached(MainCharacter mainCharacter){

        if (Creature.AudioSource != null && Creature.BarkSounds != null) {
            if (!Creature.AudioSource.enabled) Creature.AudioSource.enabled = true;
            Creature.AudioSource.pitch = Random.Range(Creature.MinPitch, Creature.MaxPitch);
            int random = (int)Random.Range(0.0f, Creature.BarkSounds.Length - 1);
            Creature.AudioSource.PlayOneShot(Creature.BarkSounds[ random ]);
        }

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

    public void PositionDog()
    {
        MainCharacter character = Creature.GameController.MainCharacter;
        int dogLoc = 0;
        foreach(Dog dog in character.DogInventory)
        {
            if (dog == this)
                break;
            if(dog.Creature.CurrentState == State.Follow || dog.Creature.CurrentState == State.Attack)
                dogLoc++;
        }

		if (character.DogInventory.Count > 0 && Creature.CurrentState == State.Follow || Creature.CurrentState == State.Attack)
        {
            
			float radians;
			if(character.NumActiveDogs > 0)
				radians = ((360 / character.NumActiveDogs) * dogLoc) * (Mathf.PI / 180.0f);
			else
				radians = 360 * (Mathf.PI / 180.0f);
			float xLoc = character.transform.position.x + (Mathf.Cos (radians) * DogDistance);
			float yLoc = character.transform.position.y + (Mathf.Sin (radians) * DogDistance) / 2.0f;

            float xMovement = (xLoc - transform.position.x) * Creature.Speed * Time.deltaTime;
            float yMovement = (yLoc - transform.position.y) * Creature.Speed * Time.deltaTime;

            Creature.Move(xMovement, yMovement);

        } else
			Debug.LogWarning ("Dog not active or not in inventory");

    }

	public void Death(){

        if (Creature.GameController.HUD.AudioSource != null && Creature.DeathSounds != null) {
            Creature.GameController.HUD.AudioSource.pitch = Random.Range(Creature.MinPitch, Creature.MaxPitch);
            Creature.GameController.HUD.AudioSource.PlayOneShot(Creature.DeathSounds[0]);
        }

        Creature.GameController.DogDeath (this);
	}

}

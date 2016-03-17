using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(CombatAI))]

public class Monster : MonoBehaviour {

    public Creature Creature;
    public CombatAI CombatAI;
    public int speed;
    private float time;
    private int direction, directionx, directiony;

	void Start ()
    {
        Creature.ChangeState(State.Idle);
    }

	void Update ()
    {
        if (Creature.CurrentState == State.Attack) {

            if (CombatAI.HasTarget)
            {
                float xShift = (CombatAI.CurrentTarget.transform.position.x - transform.position.x) * Creature.Speed*Time.deltaTime;
                float yShift = (CombatAI.CurrentTarget.transform.position.y - transform.position.y) * Creature.Speed*Time.deltaTime;
                Creature.Move(xShift, yShift);
            }
            CombatAI.TryAttackDog();

        } else if (Creature.CurrentState == State.Idle) {

            if(time<=0)
            {
                time = Random.Range(1, 10);
                direction = Random.Range(1, 5);
            }
            else
            {
                time -= Time.deltaTime;
                switch(direction)
                {
				case 5:
					directionx = 0;
					directiony = 0;
					break;
                case 4:
                    directionx = 1;
                    directiony = 0;
                    break;
                case 3:
                    directionx = -1;
                    directiony = 0;
                    break;
                case 2:
                    directiony = 1;
                    directionx = 0;
                    break;
				case 1:
                    directiony = -1;
                    directionx = 0;
                    break;
                }
                transform.Translate(new Vector3(directionx * speed * Time.deltaTime, directiony * speed * Time.deltaTime, 0));

            }

            if (CombatAI.WithinInteractionRange(Creature.GameController.MainCharacterObj))
                Creature.ChangeState(State.Attack);
		}
    }

	public void Death(){
		Creature.ChangeState (State.Dead);
		Creature.GameController.CombatController.RemoveFromCombat (CombatAI);
		Creature.GameController.LevelGeneration.RemoveMonsterFromGrid (this);
		Destroy (gameObject);
	}
}

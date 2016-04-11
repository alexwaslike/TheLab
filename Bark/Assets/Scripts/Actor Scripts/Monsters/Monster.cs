using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(CombatAI))]

public class Monster : MonoBehaviour {

    public Creature Creature;
    public CombatAI CombatAI;
    private float time;
    private int direction, directionx, directiony;

    public int NumItemsDropped;

    public Image HealthBarImage;

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
                transform.Translate(new Vector3(directionx * Creature.Speed * Time.deltaTime, directiony * Creature.Speed * Time.deltaTime, 0));

            }

			if (CombatAI.WithinRange(Creature.GameController.MainCharacterObj, CombatAI.InteractionRange))
                Creature.ChangeState(State.Attack);
		}
    }

	public void Death(){
		Creature.ChangeState (State.Dead);
		Creature.GameController.CombatController.RemoveFromCombat (CombatAI);
		Creature.GameController.LevelGeneration.RemoveFromGrid (gameObject);

        int x = 0;
        for(int i=0; i< NumItemsDropped; i++)
        {
            GameObject drop = Creature.GameController.LevelGeneration.GenItem();
            drop = Instantiate(drop, new Vector3(transform.position.x + x, transform.position.y, 0), Quaternion.identity) as GameObject;
            drop.SetActive(true);
            drop.GetComponent<Item>().GameController = Creature.GameController;
            x++;
        }

		Destroy (gameObject);
	}

    public void OnCollisionStay2D(Collision2D coll)
    {
        switch(direction)
        {
            case 5:
                directionx = 0;
                directiony = 0;
                break;
            case 4:
                direction = 3;
                break;
            case 3:
                direction = 4;
                break;
            case 2:
                direction = 1;
                break;
            case 1:
                direction = 2;
                break;
        }

    }
}

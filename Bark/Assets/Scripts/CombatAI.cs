using UnityEngine;
using System.Collections;

public class CombatAI : MonoBehaviour {

    private int _health;
    public int Health
    {
        get { return _health; }
    }

    private int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
    }

    private int _attackRate = 10;
    public int AttackRate
    {
        get { return _attackRate; }
    }

    private int _attackDamage = 10;
    public int AttackDamage
    {
        get { return _attackDamage; }
    }

    private CombatAI _currentTarget;
    public CombatAI CurrentTarget
    {
        get { return _currentTarget; }
    }

    private bool _hasTarget;
    private int _attackCooldown;

    public GameController GameController;
    public Creature Creature;
    public float InteractionRange = 2.0f;

    void Start()
    {
        Creature = GetComponent<Creature>();
        if (Creature != null)
            GameController = Creature.GameController;
        else
            GameController = GetComponent<MainCharacter>().GameController;

        _health = _maxHealth;
        _attackCooldown = 0;
        _hasTarget = false;
    }

    public bool WithinInteractionRange(GameObject otherObject)
    {
        if (Vector3.Distance(transform.position, otherObject.transform.position) <= InteractionRange)
            return true;
        return false;
    }

    public void TryAttack()
    {
        if (Creature != null)
        {
            if (!WithinInteractionRange(GameController.MainCharacterObj)){
                Creature.ChangeState(State.Idle);
            }
            else if (Creature.GameController.CanAttack()){
                if (_hasTarget)
                {
                    Attack();
                }
                else {

                    if (Creature.GameController.MainCharacter.DogInventory.Count > 0)
                        _currentTarget = GameController.MainCharacter.DogInventory[0].CombatAI;
                    else
                        _currentTarget = GameController.MainCharacter.CombatAI;
                    _hasTarget = true;
                    Attack();

                }
            }
            else {
                _hasTarget = false;
                // TODO: strafe around for a bit
            }
        }
        else Debug.Log("Attempting to use Creature Attack method with non-Creature");
        
    }

    private void Attack()
    {

        Creature.Move((_currentTarget.transform.position.x - transform.position.x) * Creature.Speed, (_currentTarget.transform.position.y - transform.position.y) * Creature.Speed);

        if (_attackCooldown == 0)
        {
            _attackCooldown = _attackRate;
            _currentTarget.TakeDamage(_attackDamage);
        }
        else {
            _attackCooldown--;
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }




}

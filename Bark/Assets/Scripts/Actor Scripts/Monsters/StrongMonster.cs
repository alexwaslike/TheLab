using UnityEngine;

public class StrongMonster : MonsterTrait {

	public Monster Monster;

	private int _damageBuff = 2;

	void Start () {
		Name = "Strong";
		Description = WritingDB.MonsterTraitDescriptions[Name];
		icon = Monster.Creature.GameController.SpriteController.dogTraitSprite_Tall;

		Monster.CombatAI.AttackDamage += _damageBuff;
	}
}

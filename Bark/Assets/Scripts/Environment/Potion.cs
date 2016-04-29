using UnityEngine;

public class Potion : Item {
    
    public override void ActivateItem()
    {
        GameController.MainCharacter.Health.Heal(StatModifier);
    }

}

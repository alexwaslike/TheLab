using UnityEngine;

public class Potion : Item {
    
    public override void ActivateItem()
    {
        if(GameController.MainCharacter.DogInventory.Count > 0) {
            Dog lowestHealthDog = GameController.MainCharacter.DogInventory[0];
            foreach (Dog doge in GameController.MainCharacter.DogInventory)
            {
                if (doge.isActiveAndEnabled && doge.Health.HP < lowestHealthDog.Health.HP)
                    lowestHealthDog = doge;
            }
            lowestHealthDog.Health.Heal(StatModifier * lowestHealthDog.Health.MaxHealth);
        }
    }

}

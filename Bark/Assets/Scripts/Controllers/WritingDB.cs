using System.Collections.Generic;

public static class WritingDB {

	public static Dictionary<string, string> DogTraitDescriptions = new Dictionary<string, string>()
	{
		{"Tall", "Ride a majestically Tall dog to victory. Sight distance increase."}
	};

	public static Dictionary<string, string> MonsterTraitDescriptions = new Dictionary<string, string>()
	{
		{"Fast", "You can hide, but you can't run from the Fast monster!"},
		{"Strong", "Your team better be buffed before you fight the buff Strong monster!"}
	};

	public static Dictionary<string, string> ItemDescriptions = new Dictionary<string, string>()
	{
		{"Super Potion", "Heals full health."},
        {"Potent Potion", "Heals half health."},
        {"Mild Potion", "Heals 1/4 health."}
    };

}

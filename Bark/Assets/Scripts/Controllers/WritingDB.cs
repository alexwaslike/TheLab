using System.Collections.Generic;

public static class WritingDB {

	public static Dictionary<string, string> DogDescriptions = new Dictionary<string, string>()
	{
		{"Pom", "It's so tiny... and... radioactive! I feel like it needs my protection otherwise it might get gobbled up by the monsters."},
        {"Mastiff", "It's so big and fluffy I might die! I can just hide in its fur if I get scared. But would I get radiation posioning?!?"},
        {"Dachshund", "Are dogs supposed to stretch like this? What have they been feeding you?!?"},
        {"Mop", "I'm not sure why this radioactive mop is following me around."},
        {"Beagle", "Just a normal dog. Kind of plain actually, other than the radioactivity."}
    };

	public static Dictionary<string, string> ItemDescriptions = new Dictionary<string, string>()
	{
		{"Super Potion", "Those purple treats are much better than the blue ones. I'm not sure why though. They certainly taste better. Heals full health."},
        {"Potent Potion", "Oh boy blue dog treats! I love the color blue, it's my favorite. Much better than orange. Heals half health."},
        {"Mild Potion", "A box of dog treats, the dogs seem to enjoy it and I bet I will too! Heals 1/4 health."},
        {"Note 0", "It has some smudged text, but this is what I can make out: \"--------- My only wish is that I could save the shelter dogs. Some of them are quite mutated and violent, and I think it's because of ---------------------------------- But the rest seem to be friendly, and --------------------------------------------- Dear son, please rescue as many of the shelter dogs as you can and take refuge in the bomb shelter.\""},
        {"Note 1", "It has some smudged text, but this is what I can make out: \"We’ve evacuated most of the city. Since we can’t force people to leave, some of the cities workers like doctors and other healthcare types stayed. ---------------------------------------------------------------- --------------------------------------------------------------------------------------------------------------------------------------------------------- Hopefully the fallout won’t be too bad and the radiation will die down in a couple days. ----------------------------------------------------------------------- Murphy\""},
        {"Note 2", "It has some smudged text, but this is what I can make out: \"The fallout seems to come in the form of rain. It seems many of the buildings were destroyed. Ive noticed the animals drinking the radioactive water. ------------------------------------------------------------ ------------------------------------------------------------------------------------------------------------------------------------------ saw a squirrel with a 3rd eye today ------------------- rapid mutation?------------------------- ------------ Highly aggressive-------------------------- Found some dogs today. Checked for mutations and aggression but they seemed to be fine. Fido is what I called the one I found today. Murphy\""},
        {"Note 3", "It has some smudged text, but this is what I can make out: \"I made my way to the bunker located across town on the other side of the local park. It was pretty clear and seemed safe. I went to make a supply run so I could stay in the shelter and start finding others and getting them to safety and I dropped the key somewhere in the park... ------------------- ------------------------------------------------------------------------------------------------------------------------------------------------------------- More rapid mutations on the wildlife and they’re still highly aggressive. ---------------------------------- dangerous to be outside…----------------------- Shark monster---------------------------- Hope I can find the key.. Fido is still with me so I’ll be safe for awhile. Murphy\""}
    };

    public static Dictionary<string, string> WinStates = new Dictionary<string, string>()
    {
        {"0", "Who's the monster here?"},
        {"1", "Boy that dog must be really lonely, why didn't you get him any friends?"},
        {"2", "The dogs seem content but it's kind of a small family don't you think? Man, talk about a third wheel."},
        {"5", "You managed to save a decent few, but you're not a dog person are you?"},
        {"10", "Dog-gone-it, you are a pup magnet!!"},
        {"15", "Wait where did you find all of these..?"},
        {"20", "You have a problem... Seriously"}
    };

}

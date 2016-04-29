using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour {

    public Text RescueNumberText;
    public Text RescueDescriptionText;

	public void PopulateText(int numDogsRescued)
    {
        RescueNumberText.text = "You were able to rescue " + numDogsRescued + " dogs.";

        if (numDogsRescued == 0)
            RescueDescriptionText.text = WritingDB.WinStates["0"];
        else if (numDogsRescued == 1)
            RescueDescriptionText.text = WritingDB.WinStates["1"];
        else if(numDogsRescued == 2)
            RescueDescriptionText.text = WritingDB.WinStates["2"];
        else if(numDogsRescued > 2 && numDogsRescued <= 5)
            RescueDescriptionText.text = WritingDB.WinStates["5"];
        else if(numDogsRescued > 5 && numDogsRescued <= 10)
            RescueDescriptionText.text = WritingDB.WinStates["10"];
        else if (numDogsRescued > 10 && numDogsRescued <= 15)
            RescueDescriptionText.text = WritingDB.WinStates["15"];
        else if (numDogsRescued > 15)
            RescueDescriptionText.text = WritingDB.WinStates["20"];

    }


}

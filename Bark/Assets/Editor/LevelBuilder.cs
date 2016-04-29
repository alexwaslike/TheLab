using UnityEngine;
using UnityEditor;

public class LevelBuilder : EditorWindow {

    string _titleString = "Level Builder";
    
    [MenuItem ("Window/Level Builder")]
    public void Init()
    {

        LevelBuilder window = (LevelBuilder)EditorWindow.GetWindow(typeof(LevelBuilder));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label(_titleString, EditorStyles.boldLabel);

    }

}

using UnityEngine;
using UnityEditor;

public class LevelBuilder : EditorWindow {

    string _titleString = "Level Builder";
    Grid grid;
    
    [MenuItem ("Window/Level Builder")]
    public void Init()
    {
        grid = FindObjectOfType<Grid>();

        LevelBuilder window = (LevelBuilder)EditorWindow.GetWindow(typeof(LevelBuilder));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label(_titleString, EditorStyles.boldLabel);

    }

}

using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Grid))]
public class GridEditor : Editor {

    Grid grid;

    public void OnEnable()
    {
        grid = (Grid)target;
        SceneView.onSceneGUIDelegate = GridUpdate;
    }
    
    void GridUpdate(SceneView sceneView)
    {
        Event e = Event.current;

        Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePosition = r.origin;

        if(e.isKey && e.character == 'a') {

            GameObject obj;

            Object prefab = PrefabUtility.GetPrefabParent(Selection.activeObject);

            if (prefab)
            {
                obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

                Vector3 closestGridPoint = grid.grid[0,0];
                foreach(Vector3 point in grid.grid)
                {
                    if (Mathf.Abs(Vector3.Distance(point, mousePosition)) < Mathf.Abs(Vector3.Distance(closestGridPoint, mousePosition)))
                        closestGridPoint = point;
                }

                Vector3 alignedWithGrid = closestGridPoint;
                obj.transform.position = alignedWithGrid;
            }

        }
    }

    public override void OnInspectorGUI()
    { 

        GUILayout.BeginVertical();

        GUILayout.Label("Start X");
        grid.StartX = EditorGUILayout.FloatField(grid.StartX, GUILayout.Width(50));

        GUILayout.Label("Start Y");
        grid.StartY = EditorGUILayout.FloatField(grid.StartY, GUILayout.Width(50));

        GUILayout.Label("Cell Width");
        grid.CellWidth = EditorGUILayout.FloatField(grid.CellWidth, GUILayout.Width(50));

        GUILayout.Label("Cell Angle");
        grid.CellAngle = EditorGUILayout.FloatField(grid.CellAngle, GUILayout.Width(50));

        GUILayout.Label("Number of cells in x dir"); 
        grid.NumCellsX = EditorGUILayout.IntField(grid.NumCellsX, GUILayout.Width(50));

        GUILayout.Label("Number of cells in y dir");
        grid.NumCellsY = EditorGUILayout.IntField(grid.NumCellsY, GUILayout.Width(50));

        GUILayout.Label("Icon size");
        grid.IconSize = EditorGUILayout.FloatField(grid.IconSize, GUILayout.Width(50));
        

        GUILayout.EndVertical();

        SceneView.RepaintAll();
    }

}

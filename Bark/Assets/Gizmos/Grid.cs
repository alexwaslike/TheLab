using UnityEngine;

public class Grid : MonoBehaviour {

    public float StartX = 0.0f;
    public float StartY = 0.0f;

    public float CellWidth = 10.0f; 
    public float CellHeight;
    public float CellAngle = 30.0f;

    public int NumCellsX = 20;
    public int NumCellsY = 10;

    public float IconSize = 0.1f;

    public Vector3[,] grid;

    void CreateGrid()
    {
        grid = new Vector3[NumCellsX, NumCellsY];

        CellHeight = (CellAngle * (Mathf.PI / 180.0f)) * CellWidth;

        bool alt = false;
        for (int x = 0; x < NumCellsX; x++) {
            for (int y = 0; y < NumCellsY; y++) {

                Vector3 newPos = Vector3.zero;

                if (alt)
                    newPos = new Vector3(x * (CellWidth / 2) + StartX, y * CellHeight + CellHeight / 2 + StartY, 0);
                else
                    newPos = new Vector3(x * (CellWidth / 2) + StartX, y * CellHeight + StartY, 0);

                grid[x, y] = newPos;

            }
            alt = !alt;
        }
    }

    void OnDrawGizmos()
    {
        CreateGrid();

        for (int x = 0; x < NumCellsX; x++) {
            for (int y = 0; y < NumCellsY; y++) {

                Gizmos.DrawSphere(grid[x,y], IconSize);

            }
        }
    }

}

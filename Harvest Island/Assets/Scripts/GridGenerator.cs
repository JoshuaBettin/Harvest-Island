using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    private Grid grid;



    // Start is called before the first frame update
    void Start()
    {
        
        grid.InstantiateGrid();
        
        /*
        grid.AddCell(GridCell.CellType.Soil);
        grid.AddCell(GridCell.CellType.Soil);
        grid.AddCell(GridCell.CellType.Soil);
        grid.AddCell(GridCell.CellType.Soil);

        grid.AddCell(GridCell.CellType.Soil);
        grid.AddCell(GridCell.CellType.Soil);
        grid.AddCell(GridCell.CellType.Soil);
        grid.AddCell(GridCell.CellType.Soil);


        for (int x = 0; x < grid.gridCellArray.GetLength(0); x++)
        {
            for (int y = 0; y < grid.gridCellArray.GetLength(1); y++)
            {
                if (grid.gridCellArray[x, y] != null)
                {
                    if (grid.gridCellArray[x, y] != null)
                    {
                        grid.gridCellArray[x, y].test();
                    }
                }
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    public void GenerateGrid()
    {
        for (int x = 0; x < gridCellArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridCellArray.GetLength(1); y++)
            {
                if (gridCellArray[x,y] != null)
                {
                    GameObject CellToInstantiate = gridCellArray[x, y].gameObject;

                    Vector3 position = new Vector3(0, 0, 0);

                    Instantiate(GameObject, position);
            }
        }
    }
    */
}

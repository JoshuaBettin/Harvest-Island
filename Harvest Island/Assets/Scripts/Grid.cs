using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridCell[,] gridCellArray;

    [SerializeField]
    public int width, height;
   
    [SerializeField]
    private GameObject SoilPrefab, SandPrefab, WaterPrefab;
    
    [SerializeField]
    public List<GameObject> cellList;

    /*
    public Grid(int width, int height, GridCell[,] gridCellArray) : this(width, height)
    {
        this.gridCellArray = gridCellArray;
    }
    */

    private void Awake()
    {
        gridCellArray = new GridCell[width, height];
        cellList = new List<GameObject>();

    }

    private void Start()
    {
        Debug.Log(cellList.Count);

        cellList.Add(WaterPrefab);
        cellList.Add(WaterPrefab);
        cellList.Add(WaterPrefab);
        cellList.Add(WaterPrefab);
        cellList.Add(WaterPrefab);
        cellList.Add(WaterPrefab);

        cellList.Add(WaterPrefab);
        cellList.Add(SandPrefab);
        cellList.Add(SandPrefab);
        cellList.Add(SandPrefab);
        cellList.Add(SandPrefab);
        cellList.Add(WaterPrefab);

        cellList.Add(WaterPrefab);
        cellList.Add(SandPrefab);
        cellList.Add(SoilPrefab);
        cellList.Add(SoilPrefab);
        cellList.Add(SandPrefab);
        cellList.Add(WaterPrefab);

        cellList.Add(WaterPrefab);
        cellList.Add(SandPrefab);
        cellList.Add(SoilPrefab);
        cellList.Add(SoilPrefab);
        cellList.Add(SandPrefab);
        cellList.Add(WaterPrefab);

        cellList.Add(WaterPrefab);
        cellList.Add(SandPrefab);
        cellList.Add(SandPrefab);
        cellList.Add(SandPrefab);
        cellList.Add(SandPrefab);
        cellList.Add(WaterPrefab);

        cellList.Add(WaterPrefab);
        cellList.Add(WaterPrefab);
        cellList.Add(WaterPrefab);
        cellList.Add(WaterPrefab);
        cellList.Add(WaterPrefab);
        cellList.Add(WaterPrefab);

        Debug.Log(cellList.Count);
    }

    public void InstantiateGrid()
    {

        int i = 0; 

        for (int x = 0; x < gridCellArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridCellArray.GetLength(1); y++)
            {
                if (gridCellArray[x, y] == null)
                {
                    if (i < cellList.Count)
                    {
                        if (cellList[i] != null)
                        {
                            GameObject current = GameObject.Instantiate(cellList[i], new Vector3(x, y, 0), Quaternion.identity, this.transform);
                            GridCell currentGridCell = current.GetComponent<GridCell>();

                            gridCellArray[x, y] = currentGridCell;

                            i++;
                        }
                    }
                }
            }
        }
    }

    public GameObject InstantiateGridCell(GridCell.CellType type)
    {
        GameObject current = null;

        switch (type)
        {
            case GridCell.CellType.Soil:
                SoilCell soilcell = null;
                current = soilcell.InstantiateCell(); 
                break;
            case GridCell.CellType.Sand:
                SoilCell sandcell = null;
                current = sandcell.InstantiateCell();
                break;
            case GridCell.CellType.Water:
                SoilCell watercell = null;
                current = watercell.InstantiateCell();
                break; 
        }

        return current; 

    }
}
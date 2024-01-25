using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
   // private float size;
  //  private CellType type;

    public enum CellType
    {
        Soil,
        Sand,
        Water
    }

    public GridCell(CellType type)
    {

    }

    public virtual void test()
    {
        Debug.Log("test");
    }
  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCell : GridCell
{
    private CellType type;

    [SerializeField]
    private GameObject cellPrefab;

    public SandCell(CellType type) : base(type)
    {
        this.type = type;
    }

    public override void test()
    {
        Debug.Log("SandCell");
    }

    public GameObject InstantiateCell()
    {
        GameObject current = GameObject.Instantiate(cellPrefab);

        return current;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridUpdater : MonoBehaviour
{
    [SerializeField]
    private Tilemap tileMap;
    [SerializeField]
    private TileBase grass, sand;
    

    private Vector3Int position = new Vector3Int(10,10);
    private Vector3Int position1 = new Vector3Int(11, 10);
    private Vector3Int position2 = new Vector3Int(12, 10);

    private float timer; 

    // Start is called before the first frame update
    void Start()
    {
        tileMap.SetTile(position, grass);
        tileMap.SetTile(position1, grass);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5 && timer < 1.0)
        {
            tileMap.SetTile(position, sand);
        }
        if (timer > 1.0)
        {
            tileMap.SetTile(position, grass);
        }

        if (timer > 1.5) timer = 0; 
    }

    public void ChangeTile(Vector3Int position, GridCell.CellType type)
    {
        if (type == GridCell.CellType.Soil)
        {
            tileMap.SetTile(position, sand);
        }
        if (type == GridCell.CellType.Sand)
        {
            tileMap.SetTile(position, grass);
        }
    }
}

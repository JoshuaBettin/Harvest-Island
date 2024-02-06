using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class GridUpdater : MonoBehaviour
{
    [SerializeField]
    private Tilemap tileMap;
    [SerializeField]
    private TileBase grass, sand;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

   // Manager für tiles -> kriegt photonview 
   // rpc
    public void ChangeTile(Vector3Int position, GridCell.CellType type)
    {
        //Vector3Int position = Vector3Int.FloorToInt(vector);

        // RPC
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

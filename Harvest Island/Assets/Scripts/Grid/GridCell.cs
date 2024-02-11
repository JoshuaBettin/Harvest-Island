using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridCell : MonoBehaviour, IPointerClickHandler
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

    
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerClick);

    }
}

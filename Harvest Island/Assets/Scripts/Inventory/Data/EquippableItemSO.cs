using Inventory.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Data
{
    [CreateAssetMenu(menuName = "EquippableItemSO")]
    public class EquippableItemSO : ItemSO, IItemAction,  IDestroyableItem
    {
        public string ActionName => "Equip";

        public AudioClip actionSFX { get; private set; }
        
        public bool PerformAction(GameObject character, List<ItemParameter> itemState)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            if(weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ? DefaultParameterList : itemState);
                return true;
            }
            return false; 
        }
    }
}
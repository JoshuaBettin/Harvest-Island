using Inventory.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Data
{
    [CreateAssetMenu(menuName = "EquippableItemSO")]
    public class EquippableItemSO : ItemSO, IItemAction, IDestroyableItem, IEquippableItem
    {
        public string ActionName => "Equip";

        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }

        [field: SerializeField]
        public AudioClip dropSFX { get; private set; }

        [field: SerializeField]
        public AudioClip breakSFX { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            if (weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ? DefaultParameterList : itemState);
                return true;
            }
            return false;
        }

        public AgentWeapon ReturnItemToEquip(GameObject character)
        {
            return character.GetComponent<AgentWeapon>();
        }
    }
}
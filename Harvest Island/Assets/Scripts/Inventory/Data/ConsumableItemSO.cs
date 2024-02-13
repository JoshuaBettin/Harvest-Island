using Inventory.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Data
{
    [CreateAssetMenu(menuName = "ConsumableItemSO")]
    public class ConsumableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Consume";

        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }

        [field: SerializeField]
        public AudioClip dropSFX { get; private set; }

        [SerializeField]
        private List<ModifierData> modifierDataList = new List<ModifierData>();

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach (ModifierData data in modifierDataList)
            {
                data.statModifier.AffectCharacter(character, data.value);
            }
            return true;
        }

    }
}
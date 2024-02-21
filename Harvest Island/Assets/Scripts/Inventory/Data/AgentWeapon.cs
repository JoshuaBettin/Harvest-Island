using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Inventory.Data
{
    public class AgentWeapon : MonoBehaviour
    {

        [SerializeField]
        private EquippableItemSO weapon;

        [SerializeField]
        private InventorySO inventoryData;

        [SerializeField]
        private List<ItemParameter> parametersToModify, itemCurrentState;

        [SerializeField]
        private InputAction inputAction;

        public EquippableItemSO Weapon { get => weapon; }

        private int durability;

        public int Durability { get => durability; }

        private Animator playerAnimator;

        private void Start()
        {
            playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }

        public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
        {
            if (Weapon != null)
            {
                inventoryData.AddItem(Weapon, 1, itemCurrentState);
            }
            this.weapon = weaponItemSO;
            this.itemCurrentState = new List<ItemParameter>(itemState);
            foreach (ItemParameter parameter in itemState)
            {
                this.durability = (int)parameter.value;
            }
        }

        public void ModifyParameters()
        {
            foreach (ItemParameter parameter in parametersToModify)
            {
                if (itemCurrentState.Contains(parameter))
                {
                    int index = itemCurrentState.IndexOf(parameter);
                    float newValue = itemCurrentState[index].value + parameter.value; //add: clamp maxvalue
                    itemCurrentState[index] = new ItemParameter
                    {
                        itemParameter = parameter.itemParameter,
                        value = newValue
                    };

                    durability = (int)newValue; // works only if only durability-parameter
                }
            }
        }

        public virtual void UseWeapon()
        {
            if (weapon != null)
            {
                this.ModifyParameters();
                PlayItemAnitmation();
                inventoryData.PlaySound(weapon.actionSFX, 1);

                if (durability <= 0)
                {
                    inventoryData.PlaySound(weapon.breakSFX, 1);
                    weapon = null;
                }




            }
        }

        public void PlayItemAnitmation()
        {
            playerAnimator.SetTrigger("Axe");

        }

    }
}
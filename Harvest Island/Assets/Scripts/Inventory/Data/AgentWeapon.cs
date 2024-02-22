using Photon.Pun;
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

        [SerializeField]
        private Transform attackPoint;

        [SerializeField]
        private float attackRange;
        [SerializeField]
        private LayerMask enemyLayers;

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
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("Damaged");

                    PhotonView thisPv = PhotonView.Get(this);
                    int thisPvID = thisPv.ViewID;

                    PhotonView hitPv = enemy.gameObject.GetPhotonView();
                    int hitPvID = hitPv.ViewID;

                    hitPv.RPC("ChangeHealth", RpcTarget.All, -10f, hitPvID, thisPvID);
                }

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

        [PunRPC]
        public void ChangeHealth(float value, int hitPvID, int thisPvID)
        {
            HealthBar healthBar = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>();
            PhotonView pv = PhotonNetwork.GetPhotonView(hitPvID);
            if(hitPvID != thisPvID) if(pv.IsMine) healthBar.ChangeHealth(value);
        }

        public void PlayItemAnitmation()
        {
            playerAnimator.SetTrigger("Axe");

        }

        public void OnDrawGizmos()
        {
            if (attackPoint == null) return; 

            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
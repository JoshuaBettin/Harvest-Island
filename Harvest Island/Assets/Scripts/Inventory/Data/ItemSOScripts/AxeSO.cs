using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventory.Data
{
    public class AxeSO : EquippableItemSO
    {
        /*
        public void SpecificUse(List<GameObject> gameObjects)
        {
            foreach (GameObject obj in gameObjects)
            {
                HealthBar healthBar = obj.GetComponent<HealthBar>();
                healthBar.ChangeHealth(-10);

            }
        }
        */
        
        // dont do it that way;
        // implement agentweapon that way that it checks kind of weapon and executes special use;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIAgentWeaponPanel : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem item;

        [SerializeField]
        private Image image;

        private void Awake()
        {
            item = GetComponentInChildren<UIInventoryItem>();
        }

        public void SetData(Sprite sprite, int durability)
        {
            image.gameObject.SetActive(true);
            item.SetData(sprite, durability);
        }

        public void SetAgentWeaponEmpty()
        {
            image.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Data
{
    [CreateAssetMenu(menuName = "ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public int ID => GetInstanceID();

        [SerializeField]
        private bool isStackable;
        [SerializeField]
        private int maxStackSize = 1;
        [SerializeField]
        private string title;

        [SerializeField]
        [TextArea]
        private string description;

        [SerializeField]
        private Sprite sprite;

        public bool IsStackable { get => isStackable; set => isStackable = value; }
        public int MaxStackSize { get => maxStackSize; set => maxStackSize = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public Sprite Sprite { get => sprite; set => sprite = value; }
    }
}

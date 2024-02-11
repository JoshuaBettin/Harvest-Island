using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Data
{
    public abstract class ItemSO : ScriptableObject
    {
        public int ID => GetInstanceID();

        [SerializeField]
        private bool isStackable;
        [SerializeField]
        private int maxStackSize = 1;
        [SerializeField]
        private string title;
        [SerializeField]
        private Sprite sprite;

        [SerializeField]
        [TextArea]
        private string description;

        [SerializeField]
        private List<ItemParameter> defaultParameterList = new List<ItemParameter>();

        public bool IsStackable { get => isStackable; set => isStackable = value; }
        public int MaxStackSize { get => maxStackSize; set => maxStackSize = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public Sprite Sprite { get => sprite; set => sprite = value; }
        public List<ItemParameter> DefaultParameterList { get => defaultParameterList; set => defaultParameterList = value; }
    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value; 

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}

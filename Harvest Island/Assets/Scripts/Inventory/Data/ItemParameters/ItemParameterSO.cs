using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Data
{
    [CreateAssetMenu(menuName = "ItemParameterSO")]
    public class ItemParameterSO : ScriptableObject
    {
        [SerializeField]
        private string parameterName;

        public string ParameterName { get => parameterName; private set => parameterName = value; }
    }
}

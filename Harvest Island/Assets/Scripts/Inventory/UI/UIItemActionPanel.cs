using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIItemActionPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject buttonPrefab;

        public void AddButton(string name, Action onClickAction)
        {
            GameObject button = Instantiate(buttonPrefab, this.transform);
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
            button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
        }

        public void Toggle(bool value)
        {
            RemoveOldButtons();
            gameObject.SetActive(value);
        }

        private void RemoveOldButtons()
        {
            foreach (Transform transformOfChildren in this.transform)
            {
                Destroy(transformOfChildren.gameObject);
            }
        }
    }
}


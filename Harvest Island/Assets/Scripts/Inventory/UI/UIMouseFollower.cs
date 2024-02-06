using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class UIMouseFollower : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private UIInventoryItem item;

        private void Awake()
        {
            canvas = transform.root.GetComponent<Canvas>();
            item = GetComponentInChildren<UIInventoryItem>();
        }

        public void SetData(Sprite sprite, int quantity)
        {
            item.SetData(sprite, quantity);
        }

        private void Update()
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition,
                                                                    canvas.worldCamera, out position);
            Vector3 position3 = position;
            position3.z = -10;
            transform.position = canvas.transform.TransformPoint(position3);
        }

        public void Toggle(bool value)
        {
            Debug.Log($"Item toggled: {value}");
            gameObject.SetActive(value);
        }

    }
}
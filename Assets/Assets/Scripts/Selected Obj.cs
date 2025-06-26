using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shaurya_Sample
{
    public class SelectedObj : MonoBehaviour
    {
        public GameObject[] obj;
        public Button next;
        public Button previous;
        public Button[] selectionButtons;

        private int index = 0;

        private void Start()
        {
            for (int i = 0; i < selectionButtons.Length; i++)
            {
                int buttonIndex = i;
                selectionButtons[i].onClick.AddListener(() => SelectObject(buttonIndex));
            }

            UpdateObjSelection();
        }

        public void Next()
        {
            if (index < obj.Length - 1)
            {
                index++;
                UpdateObjSelection();
            }
        }

        public void Prev()
        {
            if (index > 0)
            {
                index--;
                UpdateObjSelection();
            }
        }

        public void SelectObject(int newIndex)
        {
            if (newIndex >= 0 && newIndex < obj.Length)
            {
                index = newIndex;
                UpdateObjSelection();
            }
        }

        private void UpdateObjSelection()
        {
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].SetActive(false);
            }
            obj[index].SetActive(true);

            previous.interactable = index > 0;
            next.interactable = index < obj.Length - 1;
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class ChoosePanel : MonoBehaviour
    {
        public Button btnBack;
        
        private void Awake()
        {
             btnBack.onClick.AddListener((
                 () =>
                 {
                     gameObject.SetActive(false);
                 }
                 ));
        }
    }
}
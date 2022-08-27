using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUI.Logic
{
    public class UiInputHandler : MonoBehaviour
    {
        [SerializeField] private GameWindowsHandler _windowsHandler;

        private const string INVENTORY_BUTTON_NAME = "Inventory Button";

        private void Update()
        {
            if (Input.GetButtonDown(INVENTORY_BUTTON_NAME))
            {
                _windowsHandler.SwitchWindowState("InventoryWindow");
            }
        }
    }
}


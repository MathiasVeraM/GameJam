using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Assets.Scripts; // si usas TextMeshPro

public class InventoryUI : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI inventoryText; // Usa Text si no usas TMP

    void Update()
    {
        UpdateInventoryDisplay();
    }

    void UpdateInventoryDisplay()
    {
        if (player == null || inventoryText == null)
            return;

        List<ItemType> items = player.GetInventory();
        string content = "Inventario:\n";

        foreach (var item in items)
        {
            content += "- " + item.ToString() + "\n";
        }

        inventoryText.text = content;
    }
}

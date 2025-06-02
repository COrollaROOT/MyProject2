using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>();

    public void AddItem(string itemName, int amount = 1)
    {
        if (items.ContainsKey(itemName))
            items[itemName] += amount;
        else
            items[itemName] = amount;

        Debug.Log($"[�κ��丮] {itemName} x{amount} ȹ�� (��: {items[itemName]})");
    }

    public int GetItemCount(string itemName)
    {
        return items.TryGetValue(itemName, out int count) ? count : 0;
    }
}

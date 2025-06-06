﻿using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class ItemData
    {
        public string itemName;
        public ItemType itemType;
        public Sprite icon;
    }

    public List<ItemData> itemDatabase;  // 인스펙터에 등록
    private Dictionary<string, ItemData> itemDict;

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;

    private Dictionary<string, int> items = new Dictionary<string, int>();
    private Dictionary<string, ItemSlotUI> slotUIs = new Dictionary<string, ItemSlotUI>();

    private void Awake()
    {
        itemDict = new Dictionary<string, ItemData>();
        foreach (var item in itemDatabase)
        {
            Debug.Log($"[Inventory] 등록된 아이템: {item.itemName}");
            itemDict[item.itemName] = item;
        }
    }

    public enum ItemType
    {
        Weapon,
        Resource,
        Coin
    }


    public bool HasItem(string itemName, int amount = 1)
    {
        return items.ContainsKey(itemName) && items[itemName] >= amount;
    }

    public void UseItem(string itemName, int amount = 1)
    {
        if (!items.ContainsKey(itemName))
        {
            Debug.LogWarning($" '{itemName}' 아이템이 인벤토리에 없습니다. 사용 불가.");
            return;
        }

        items[itemName] -= amount;

        if (items[itemName] <= 0)
        {
            items.Remove(itemName);
        }

        UpdateUI(itemName);
    }

    public void AddItem(string itemName, int amount = 1)
    {
        if (!itemDict.ContainsKey(itemName))
        {
            Debug.LogWarning($"❗ Inventory에 '{itemName}' 데이터가 없습니다. UI 생략.");
            return;
        }

        if (items.ContainsKey(itemName))
            items[itemName] += amount;
        else
            items[itemName] = amount;

        UpdateUI(itemName);
    }

    private void UpdateUI(string itemName)
    {
        if (!itemDict.ContainsKey(itemName))
        {
            Debug.LogWarning($" '{itemName}'은 아이템 데이터베이스에 없습니다.");
            return;
        }

        if (!items.ContainsKey(itemName))
        {
            Debug.LogWarning($" '{itemName}' 아이템은 인벤토리 목록에 없습니다.");
            return;
        }

        if (!slotUIs.TryGetValue(itemName, out ItemSlotUI slotUI))
        {
            GameObject slotObj = Instantiate(slotPrefab, slotParent);
            slotUI = slotObj.GetComponent<ItemSlotUI>();
            slotUIs[itemName] = slotUI;
        }

        var item = itemDict[itemName];
        slotUI.SetItem(item.icon, items[itemName]);
    }
}

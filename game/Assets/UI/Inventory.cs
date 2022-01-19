using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    List<Item> items;

    public Inventory() {
        items = new List<Item>();

        /*AddItem(new Item { itemType = Item.Type.AttackBoost, amt = 1 });
        AddItem(new Item { itemType = Item.Type.JumpBoost, amt = 1 });
        AddItem(new Item { itemType = Item.Type.SpeedBoost, amt = 1 });*/
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemInInventory = false;
            foreach(Item inventoryItem in items)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amt += item.amt;
                    itemInInventory = true; 
                }
            }
            if (!itemInInventory)
            {
                items.Add(item);
            }
        }
        else
        {
            items.Add(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in items)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amt -= item.amt;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amt <= 0)
            {
                items.Remove(item);
            }
        }
        else
        {
            items.Remove(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }


    public List<Item> GetItemList()
    {
        return items;
    }
}

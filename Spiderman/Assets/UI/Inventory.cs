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
        items.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return items;
    }
}

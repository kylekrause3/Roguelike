using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    List<Item> items;

    public Inventory() {
        items = new List<Item>();

        AddItem(new Item { itemType = Item.Type.AttackBoost, amt = 1 });
        AddItem(new Item { itemType = Item.Type.JumpBoost, amt = 1 });
        AddItem(new Item { itemType = Item.Type.SpeedBoost, amt = 1 });
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public List<Item> GetItemList()
    {
        return items;
    }
}

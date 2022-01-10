using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour 
{
    Inventory inventory;
    Transform itemSlotContainer;
    Transform itemSlotTemplate;

    private void Start()
    {
        itemSlotContainer = transform.Find("Slots");
        itemSlotTemplate = itemSlotContainer.Find("SlotTemplate");
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }


    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }


        float x = -4.5f;
        int y = 0;
        float itemSlotCellSize = 50f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>(); //put slot in ui
            itemSlotRectTransform.gameObject.SetActive(true);   //Make it visible
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);

            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.getSprite();

            x++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        GameObject prefab = Instantiate(item.getPrefab(), position, Quaternion.identity);
        ItemWorld itemWorld = prefab.GetComponent<ItemWorld>();
        //itemWorld.addTrigger();
        itemWorld.SetItem(item);

        return itemWorld;
    }


    public GameObject prefab;
    private Item item;
    

    private void Start()
    {
        
    }

    /*private void addTrigger()
    {
        prefab.AddComponent<SphereCollider>();
        float largest = 0;
        BoxCollider col = prefab.GetComponent<BoxCollider>();
        for (int i = 0; i < 3; i++)
        {
            if (col.size[i] > largest) largest = col.size[i];
        }
        prefab.GetComponent<SphereCollider>().radius = (largest + 1f) / 2;
        prefab.GetComponent<SphereCollider>().isTrigger = true;
    }*/

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

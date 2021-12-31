using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
   public enum Type
    {
        //see ItemAssets when adding new items
        JumpBoost,
        SpeedBoost,
        RegenBoost,
        AttackBoost,
    }

    public Type itemType;
    public int amt;
    //public int/string rarity;

    public Sprite getSprite()
    {
        //for UI_Inventory
        switch (itemType)
        {
            default:
                case Type.JumpBoost:    return ItemAssets.Instance.JumpBoostSprite;
                case Type.SpeedBoost:   return ItemAssets.Instance.SpeedBoostSprite;
                case Type.RegenBoost:   return ItemAssets.Instance.RegenBoostSprite;
                case Type.AttackBoost:  return ItemAssets.Instance.AttackBoostSprite;
        }
    }

    public Transform getTransform()
    {
        switch (itemType)
        {
            default:
            case Type.JumpBoost: return ItemAssets.Instance.JumpBoostPrefab;
            case Type.SpeedBoost: return ItemAssets.Instance.SpeedBoostPrefab;
            case Type.RegenBoost: return ItemAssets.Instance.RegenBoostPrefab;
            case Type.AttackBoost: return ItemAssets.Instance.AttackBoostPrefab;
        }
    }

}

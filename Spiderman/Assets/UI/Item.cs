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

    public Sprite GetSprite()
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
}

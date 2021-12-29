using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
   public enum Type
    {
        JumpBoost,
        SpeedBoost,
        RegenBoost,
        AttackBoost,
    }

    public Type itemType;
    public int amt;
}

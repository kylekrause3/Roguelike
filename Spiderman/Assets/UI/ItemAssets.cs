using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    
    public static ItemAssets Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }

    #region assets
    //where we write all of our asset sprites
    //see item: getSprite when adding more sprites
    public Sprite JumpBoostSprite;
    public Sprite SpeedBoostSprite;
    public Sprite RegenBoostSprite;
    public Sprite AttackBoostSprite;
    #endregion
}

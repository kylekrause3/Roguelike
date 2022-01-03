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
    //for inventory
    //where we write all of our asset sprites
    //see item: getSprite when adding more sprites
    public Sprite JumpBoostSprite;
    public Sprite SpeedBoostSprite;
    public Sprite RegenBoostSprite;
    public Sprite AttackBoostSprite;
    #endregion

    #region 3d Prefabs
    public Transform JumpBoostPrefab;
    public Transform SpeedBoostPrefab;
    public Transform RegenBoostPrefab;
    public Transform AttackBoostPrefab;
    #endregion
}

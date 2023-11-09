using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "Scriptble Object/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("# Main Info")] 
    public WeaponType weaponType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")] 
    public int level;
    public float baseDamage;
    public int baseCount;
    public int basePer;
    public int baseSpeed;
    public float[] damages;
    public int[] counts;
    public int[] pers;
    public int[] speeds;

    [Header("# Weapon")] 
    public GameObject projectile;
}

public enum ItemType
{
    Melee,
    Range,
    Glove,
    Shoe,
    Heal,
}

using System;
using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{
    public WeaponType WeaponType { get; protected set; }
    public int Level { get; private set; }
    public float Damage { get; protected set; }
    public int Per { get; protected set; }
    public int Count { get; protected set; }
    public float Speed { get; protected set; }
    
    public GameObject asset;

    public void LevelUp(ItemData data)
    {
        SetWeaponInfo(data);
        WeaponAction();
    }
    

    void SetWeaponInfo(ItemData data)
    {
        WeaponType = data.weaponType;
        Damage = data.baseDamage;
        Count = data.baseCount;
        asset = data.projectile;
        Speed = data.baseSpeed;

    }
    protected abstract void WeaponAction();
}

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
        Damage = data.baseDamage + data.damages[GameManager.Instance.playerInfo.level];
        Count = data.baseCount + data.counts[GameManager.Instance.playerInfo.level];
        Speed = data.baseSpeed + data.speeds[GameManager.Instance.playerInfo.level];
        asset = data.projectile;

    }
    protected abstract void WeaponAction();
}
